using UnityEngine;
using static SharedConsts.ConvertValues;
using static SharedConsts.InputAxis;
using static SonicConsts.Physics;

[RequireComponent(typeof(SpriteRenderer))]
public class SonicController : MonoBehaviour
{
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
  private SpriteRenderer _spriteRenderer;

  // Flags
  private GroundSide _groundSide;
  private GroundSide _prevGroundSide;
  private SonicSizeMode _sizeMode;
  private SonicState _state;
  private SonicState _prevState;
  private bool _postWallDetachInputLock;

  [Header("Sensors")]
  public Vector3 TopUDFLengths = new(0.3f, 0.3f, 0.5f);
  public Vector3 BottomUDFLengths = new(0.3f, 0.1f, 0.5f);
  [Header("Physics")]
  public bool GravityEnabled = true;
  public float GroundedPositionOffset = 0.05f;
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
  }

  private void FixedUpdate()
  {
    _timerSystem.Update(Time.deltaTime);
    _inputSystem.Update(!_postWallDetachInputLock);

    _prevGroundSide = _groundSide;
    _groundSide = _relativeGroundInfo.GetAbsoluteSide(_groundSide);

    _sensorSystem.Update(_sizeMode, _groundSide, transform.position, TopUDFLengths, BottomUDFLengths);
    _sensorSystem.DetectGround(!_spriteRenderer.flipX, _groundLayer);

    _relativeGroundInfo.Update(_sensorSystem.GroundDetectionResult.AngleDeg);
    _groundAngleDeg = _groundSide.GetAngle(_relativeGroundInfo.AngleDeg);

    _prevState = _state;

    if (_sensorSystem.GroundDetectionResult.Detected)
    {
      _state = SonicState.Grounded;
      _playerSpeedSystem.SetSpeed(PlayerSpeedContext.GetGrounded(
        _prevState.HasFlag(SonicState.Grounded), _groundAngleDeg, _sensorSystem.GroundDetectionResult.Distance.Value));
    }
    else
    {
      _state = SonicState.Airborne;
      _playerSpeedSystem.SetSpeed(PlayerSpeedContext.GetAirborne(
        _prevState.HasFlag(SonicState.Grounded)));
    }

    UpdatePosition();
  }

  private void UpdatePosition()
  {
    var speedX = _playerSpeedSystem.SpeedX;
    var speedY = _playerSpeedSystem.SpeedY;

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
