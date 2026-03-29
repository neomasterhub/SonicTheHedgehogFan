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

  private readonly LayerMask _groundLayer;
  private readonly ConditionalValueProvider<GravitySpeed> _gravitySpeedProvider;
  private readonly ConditionalValueProvider<float> _slopeFactorSpeedProvider;
  private readonly ConditionalValueProvider<Vector2> _groundToAirSpeedProvider;
  private readonly PlayerInputSystem _inputSystem;
  private readonly PlayerSpeedConfig _playerSpeedConfig;
  private readonly PlayerSpeedSystem _playerSpeedSystem;
  private readonly RelativeGroundInfo _relativeGroundInfo;
  private readonly SonicSensorSystem _sensorSystem;
  private readonly TimerSystem _timerSystem;
  private float _groundAngleDeg;
  private float _groundAngleRad;
  private GroundDetectionResult _lastGroundDetectionResult;
  private SpriteRenderer _spriteRenderer;

  // Flags
  private bool _isGrounded;
  private bool _prevIsGrounded;
  private bool _postWallDetachInputLock;
  private GroundSide _groundSide;
  private GroundSide _prevGroundSide;
  private SonicSizeMode _sizeMode;
  private SonicState _state;
  private SonicState _prevState;

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
    _slopeFactorSpeedProvider = new();
    _groundToAirSpeedProvider = new();
    _inputSystem = new(() => Input.GetAxis(Horizontal), () => Input.GetAxis(Vertical));
    _playerSpeedConfig = new(TopSpeed, FrictionSpeed, AccelerationSpeed, DecelerationSpeed, AirTopSpeed, AirAccelerationSpeed, MaxFallSpeed, _inputDeadZone, _skiddingSpeedDeadZone);
    _playerSpeedSystem = new(_inputSystem, _playerSpeedConfig, _gravitySpeedProvider, _slopeFactorSpeedProvider, _groundToAirSpeedProvider);
    _relativeGroundInfo = new();
    _sensorSystem = new();
    _timerSystem = new();
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

    _timerSystem.Update(Time.deltaTime);
    _inputSystem.Update(!_postWallDetachInputLock);
    _sensorSystem.Update(_sizeMode, _groundSide, transform.position, TopUDFLengths, BottomUDFLengths);

    _prevIsGrounded = _prevState.HasFlag(SonicState.Grounded);
    _groundSide = _relativeGroundInfo.GetAbsoluteSide(_groundSide);

    PlayerSpeedContext playerSpeedContext;

    var groundDetectionResult = _sensorSystem.DetectGround(!_spriteRenderer.flipX, _groundLayer);
    if (groundDetectionResult != null)
    {
      _lastGroundDetectionResult = groundDetectionResult.Value;
      _isGrounded = true;
      _relativeGroundInfo.Update(_lastGroundDetectionResult.AngleDeg);
      _groundAngleDeg = _groundSide.GetAngle(_relativeGroundInfo.AngleDeg);
      _groundAngleRad = _groundAngleDeg * Mathf.Deg2Rad;
      _state = SonicState.Grounded;
      playerSpeedContext = PlayerSpeedContext.GetGrounded(
        _prevIsGrounded, _groundAngleRad, _lastGroundDetectionResult.Distance);
    }
    else
    {
      _relativeGroundInfo.Update(0);
      _isGrounded = false;
      _groundAngleDeg = 0;
      _groundAngleRad = 0;
      _state = SonicState.Airborne;
      playerSpeedContext = PlayerSpeedContext.GetAirborne(_prevIsGrounded);
    }

    _playerSpeedSystem.SetSpeed(playerSpeedContext);

    UpdatePosition();
  }

  private void InitializeSpeedSystemProviders()
  {
    var gravitySpeed = new GravitySpeed(GravityUpSpeed, GravityDownSpeed);

    _gravitySpeedProvider
      .When(() => GravityEnabled && _groundSide == GroundSide.Down, () => gravitySpeed);
  }

  private void UpdatePosition()
  {
    var speedX = _playerSpeedSystem.SpeedX;
    var speedY = _playerSpeedSystem.SpeedY;

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
