using UnityEngine;
using static SharedConsts.ConvertValues;
using static SharedConsts.InputAxis;
using static SonicConsts.Physics;

[RequireComponent(typeof(SpriteRenderer))]
public class SonicController : MonoBehaviour
{
  private const float _groundedPositionUpwardOffset = 0.05f;
  private const float _inputDeadZone = 0.001f;
  private const float _skiddingSpeedDeadZone = 0.1f;

  private readonly ConditionalValueProvider<float> _slopeFactorSpeedProvider;
  private readonly ConditionalValueProvider<Vector2> _groundToAirSpeedProvider;
  private readonly ConditionalValueProvider<GravitySpeed> _gravitySpeedProvider;
  private readonly LayerMask _groundLayer;
  private readonly PlayerInputSystem _inputSystem;
  private readonly PlayerSpeedConfig _speedConfig;
  private readonly PlayerSpeedSystem _speedSystem;
  private readonly RelativeGroundInfo _relativeGroundInfo;
  private readonly SonicSensorSystem _sensorSystem;
  private readonly TimerSystem _timerSystem;

  private bool _isGrounded;
  private bool _prevIsGrounded;
  private bool _postWallDetachInputLock;
  private float _groundAngleDeg;
  private float _groundAngleRad;
  private GroundSide _groundSide;
  private GroundSide _prevGroundSide;
  private GroundDetectionResult _lastGroundDetectionResult;
  private SonicSizeMode _sizeMode;
  private SonicState _state;
  private SonicState _prevState;
  private SpriteRenderer _spriteRenderer;

  [Header("Sensors")]
  public Vector3 TopUDFLengths = new(0.3f, 0.3f, 0.5f);
  public Vector3 BottomUDFLengths = new(0.3f, 0.1f, 0.5f);
  [Header("Physics")]
  public bool GravityEnabled = true;
  public Vector2 WallToAirSpeedDelta = new(0.011f, 0.0f);
  public Vector2 WallDetachPositionOffset = new(-0.1f, 0.0f);

  public SonicController()
  {
    _groundLayer = 8;

    _gravitySpeedProvider = new();
    _groundToAirSpeedProvider = new();
    _slopeFactorSpeedProvider = new();

    _relativeGroundInfo = new();
    _sensorSystem = new();
    _timerSystem = new();

    _inputSystem = new(() => Input.GetAxis(Horizontal), () => Input.GetAxis(Vertical));
    _speedConfig = new(TopSpeed, FrictionSpeed, AccelerationSpeed, DecelerationSpeed, AirTopSpeed, AirAccelerationSpeed, MaxFallSpeed, _inputDeadZone, _skiddingSpeedDeadZone);
    _speedSystem = new(_inputSystem, _speedConfig, _gravitySpeedProvider, _slopeFactorSpeedProvider, _groundToAirSpeedProvider);
  }

  private void OnDrawGizmos()
  {
    _sensorSystem.Draw();
  }

  private void Awake()
  {
    Application.targetFrameRate = FramePerSec;
    Time.fixedDeltaTime = 1f / FramePerSec;

    _spriteRenderer = GetComponent<SpriteRenderer>();

    InitializeSpeedSystemProviders();
  }

  private void FixedUpdate()
  {
    _prevState = _state;
    _prevGroundSide = _groundSide;
    _prevIsGrounded = _isGrounded;

    _timerSystem.Update(Time.deltaTime);
    _inputSystem.Update(!_postWallDetachInputLock);
    _sensorSystem.Update(_sizeMode, _groundSide, transform.position, TopUDFLengths, BottomUDFLengths);

    InitializeState();
    UpdatePosition();
  }

  private void InitializeSpeedSystemProviders()
  {
    var gravitySpeed = new GravitySpeed(GravityUpSpeed, GravityDownSpeed);
    var defaultGravitySpeed = new GravitySpeed(0, 0);

    _gravitySpeedProvider
      .When(() => GravityEnabled && _groundSide == GroundSide.Down, () => gravitySpeed);

    _slopeFactorSpeedProvider
      .When(() => _groundSide == GroundSide.Down, () => SlopeFactor * Mathf.Sin(_relativeGroundInfo.AngleRad))
      .When(() => _groundSide == GroundSide.Left, () => _relativeGroundInfo.AngleRad <= 0 ? SlopeFactor : SlopeFactor * Mathf.Cos(_relativeGroundInfo.AngleRad))
      .When(() => _groundSide == GroundSide.Right, () => _relativeGroundInfo.AngleRad >= 0 ? SlopeFactor : SlopeFactor * Mathf.Cos(_relativeGroundInfo.AngleRad));

    _groundToAirSpeedProvider
      .When(() => _prevGroundSide == GroundSide.Right, () => WallToAirSpeedDelta + new Vector2(-_speedSystem.SpeedY, _speedSystem.SpeedX))
      .When(() => _prevGroundSide == GroundSide.Left, () => WallToAirSpeedDelta + new Vector2(_speedSystem.SpeedY, -_speedSystem.SpeedX));

    _gravitySpeedProvider.DefaultProvider = () => defaultGravitySpeed;
    _groundToAirSpeedProvider.DefaultProvider = () => new(_speedSystem.SpeedX, _speedSystem.SpeedY);
  }

  private void InitializeState()
  {
    var groundDetectionResult = _sensorSystem.DetectGround(!_spriteRenderer.flipX, _groundLayer);

    if (groundDetectionResult != null)
    {
      _lastGroundDetectionResult = groundDetectionResult.Value;
      InitializeState_Grounded();
    }
    else
    {
      InitializeState_Airborne();
    }
  }

  private void InitializeState_Airborne()
  {
    _isGrounded = false;
    _state = SonicState.Airborne;

    _relativeGroundInfo.Update(0);
    _groundAngleDeg = 0;
    _groundAngleRad = 0;
    _groundSide = GroundSide.Down;

    _speedSystem.SetSpeed(PlayerSpeedContext.GetAirborne(_prevIsGrounded));
  }

  private void InitializeState_Grounded()
  {
    _isGrounded = true;
    _state = SonicState.Grounded;

    _relativeGroundInfo.Update(_lastGroundDetectionResult.AngleDeg);
    _groundAngleDeg = _groundSide.GetAngle(_relativeGroundInfo.AngleDeg);
    _groundAngleRad = _groundAngleDeg * Mathf.Deg2Rad;
    _groundSide = _relativeGroundInfo.GetAbsoluteSide(_groundSide);

    _speedSystem.SetSpeed(PlayerSpeedContext.GetGrounded(_prevIsGrounded, _groundAngleRad, _lastGroundDetectionResult.Distance));
  }

  private void UpdatePosition()
  {
    var speedX = _speedSystem.SpeedX;
    var speedY = _speedSystem.SpeedY;

    if (_isGrounded)
    {
      // Snap to ground with small upward offset.
      // Keeps surface normal aligned with slope.
      speedY -= (_lastGroundDetectionResult.Distance
        * (int)_lastGroundDetectionResult.SensorGroundSide)
        - _groundedPositionUpwardOffset;
    }

    // SpeedX, SpeedY - offsets in units per frame.
    var pos = transform.position + _groundSide switch
    {
      GroundSide.Down => new Vector3(speedX, speedY),
      GroundSide.Right => new Vector3(-speedY, speedX),
      GroundSide.Up => new Vector3(-speedX, -speedY),
      GroundSide.Left => new Vector3(speedY, -speedX),
      _ => throw _groundSide.ArgumentOutOfRangeException(),
    };

    transform.position = new Vector3(pos.x.Round(2), pos.y.Round(2), transform.position.z);
  }
}
