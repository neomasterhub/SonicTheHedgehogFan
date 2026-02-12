using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class SonicController : MonoBehaviour
{
  private readonly SonicSensorSystem _sonicSensorSystem = new();

  // Components
  private Animator _animator;
  private SpriteRenderer _spriteRenderer;

  // Managers
  private InputInfo _inputInfo;
  private PlayerSpeedManager _playerSpeedManager;
  private PlayerViewManager _playerViewManager;

  // States
  private GroundSide _groundSide = GroundSide.Down;
  private PlayerState _playerState = PlayerState.Grounded;
  private SonicSizeMode _sonicSizeMode = SonicSizeMode.Big;

  [Header("Animations")]
  public float MinAnimatorWalkingSpeed = 0.5f;

  [Header("Physics")]
  public float TopSpeed = SonicConsts.Physics.TopSpeed;
  public float FrictionSpeed = SonicConsts.Physics.FrictionSpeed;
  public float AccelerationSpeed = SonicConsts.Physics.AccelerationSpeed;
  public float DecelerationSpeed = SonicConsts.Physics.AccelerationSpeed;
  public float AirTopSpeed = SonicConsts.Physics.AirAccelerationSpeed;
  public float AirAccelerationSpeed = SonicConsts.Physics.AirAccelerationSpeed;
  public float MaxFallSpeed = SonicConsts.Physics.MaxFallSpeed;
  public float GravityUpSpeed = SonicConsts.Physics.GravityUp;
  public float GravityDownSpeed = SonicConsts.Physics.GravityDown;
  public float SlopeFactor = SonicConsts.Physics.SlopeFactor;
  public float GroundSpeedDeadZone = 0.05f;
  public float InputDeadZone = 0.001f;
  public bool GravityDownEnabled = true;

  [Header("Ground")]
  public LayerMask GroundLayer = 8;

  /// <summary>
  /// Keeps surface normal aligned with slope.
  /// </summary>
  public float GroundPositionOffset = SonicConsts.Sensors.Length / 2;

  [Header("UI")]
  public float GroundNormalLength = 1.5f;
  public float SensorLength = SonicConsts.Sensors.Length;
  public float SensorBeginRadius = 0.03f;
  public float SensorEndRadius = 0.01f;

  private PlayerSpeedInput PlayerSpeedInput => new()
  {
    DistanceToGround = _sonicSensorSystem.ABResult.Distance,
    GroundAngleRad = _sonicSensorSystem.ABResult.AngleRad,
    GroundSensorLength = SensorLength,
    TopSpeed = TopSpeed,
    FrictionSpeed = FrictionSpeed,
    AccelerationSpeed = AccelerationSpeed,
    DecelerationSpeed = DecelerationSpeed,
    AirTopSpeed = AirTopSpeed,
    AirAccelerationSpeed = AirAccelerationSpeed,
    MaxFallSpeed = MaxFallSpeed,
    GravityUpSpeed = GravityUpSpeed,
    GravityDownSpeed = GravityDownSpeed,
    GroundSpeedDeadZone = GroundSpeedDeadZone,
    InputDeadZone = InputDeadZone,
    GravityDownEnabled = GravityDownEnabled,
    SlopeFactor = SlopeFactor,
  };

  private PlayerViewInput PlayerViewInput => new()
  {
    TopSpeed = TopSpeed,
    MinAnimatorWalkingSpeed = MinAnimatorWalkingSpeed,
  };

  private void Awake()
  {
    Application.targetFrameRate = Consts.ConvertValues.FramePerSec;
    Time.fixedDeltaTime = 1f / Consts.ConvertValues.FramePerSec;

    _animator = GetComponent<Animator>();
    _spriteRenderer = GetComponent<SpriteRenderer>();

    _inputInfo = new InputInfo(
      () => Input.GetAxis(Consts.InputAxis.Horizontal),
      () => Input.GetAxis(Consts.InputAxis.Vertical));
    _playerSpeedManager = new PlayerSpeedManager(_inputInfo);
    _playerViewManager = new PlayerViewManager(
      _animator,
      _inputInfo,
      _playerSpeedManager,
      _spriteRenderer);
  }

  private void FixedUpdate()
  {
    UpdateInput();
    RunSensors();
    UpdateState();
    SetSpeed();
    UpdateView();
    UpdatePosition();
  }

  private void OnDrawGizmos()
  {
    _sonicSensorSystem.DrawSensors(SensorBeginRadius, SensorEndRadius);
    _sonicSensorSystem.DrawGroundNormal(GroundNormalLength, SensorBeginRadius, SensorEndRadius);
  }

  private void UpdateInput()
  {
    _inputInfo.Update();
  }

  private void RunSensors()
  {
    _sonicSensorSystem.Update(transform.position, _sonicSizeMode, _groundSide, SensorLength);
    _sonicSensorSystem.ApplyAB(GroundLayer, SensorLength);
  }

  private void UpdateState()
  {
    var playerState = PlayerState.None;

    if (_sonicSensorSystem.ABResult.GroundDetected)
    {
      playerState |= PlayerState.Grounded;
    }
    else
    {
      playerState |= PlayerState.Airborne;
    }

    Debug.Log(playerState);

    _playerState = playerState;
  }

  public void SetSpeed()
  {
    _playerSpeedManager.SetSpeed(_playerState, PlayerSpeedInput);
  }

  public void UpdateView()
  {
    _playerViewManager.Update(PlayerViewInput);
  }

  private void UpdatePosition()
  {
    // Snap to ground with small upward offset.
    var speedY = _playerSpeedManager.SpeedY;
    if (_playerState.HasFlag(PlayerState.Grounded))
    {
      speedY -= (_sonicSensorSystem.ABResult.Distance
        * _sonicSensorSystem.ABResult.SensorDirectionSign)
        - GroundPositionOffset;
    }

    // Speed X, Y - offsets in units per frame.
    transform.position += new Vector3(_playerSpeedManager.SpeedX, speedY);
  }
}
