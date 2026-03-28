using UnityEngine;
using static SharedConsts.ConvertValues;
using static SharedConsts.InputAxis;
using static SonicConsts.Physics;

[RequireComponent(typeof(SpriteRenderer))]
public class SonicController : MonoBehaviour
{
  private readonly ConditionalValueProvider<GravitySpeed> _gravitySpeedProvider = new();
  private readonly ConditionalValueProvider<float> _slopeFactorSpeedProvider = new();
  private readonly ConditionalValueProvider<Vector2> _groundToAirSpeedProvider = new();
  private readonly LayerMask _groundLayer = 8;
  private readonly PlayerInputSystem _inputSystem = new(
    () => Input.GetAxis(Horizontal),
    () => Input.GetAxis(Vertical));
  private readonly PlayerSpeedConfig _playerSpeedConfig = new(
    TopSpeed,
    FrictionSpeed,
    AccelerationSpeed,
    DecelerationSpeed,
    AirTopSpeed,
    AirAccelerationSpeed,
    MaxFallSpeed,
    0.001f,
    0.1f);
  private readonly PlayerSpeedSystem _playerSpeedSystem;
  private readonly RelativeGroundInfo _relativeGroundInfo = new();
  private readonly SonicSensorSystem _sensorSystem = new();
  private readonly TimerSystem _timerSystem = new();
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

  public SonicController()
  {
    _playerSpeedSystem = new(_inputSystem, _playerSpeedConfig, _gravitySpeedProvider, _slopeFactorSpeedProvider, _groundToAirSpeedProvider);
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
    _state = _sensorSystem.GroundDetectionResult.Detected ? SonicState.Grounded : SonicState.Airborne;
  }
}
