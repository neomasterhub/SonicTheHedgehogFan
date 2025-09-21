using UnityEngine;

public class SonicController : MonoBehaviour
{
  private readonly SonicSensorSystem _sonicSensorSystem = new();

  private InputInfo _inputInfo;
  private PlayerSpeedManager _playerSpeedManager;

  private GroundSide _groundSide = GroundSide.Down;
  private PlayerState _playerState = PlayerState.Grounded;
  private SonicSizeMode _sonicSizeMode = SonicSizeMode.Big;

  [Header("Physics")]
  public float TopSpeed = SonicConsts.Physics.TopSpeed;
  public float FrictionSpeed = SonicConsts.Physics.FrictionSpeed;
  public float AccelerationSpeed = SonicConsts.Physics.AccelerationSpeed;
  public float DecelerationSpeed = SonicConsts.Physics.AccelerationSpeed;
  public float AirTopSpeed = SonicConsts.Physics.AirAccelerationSpeed;
  public float AirAccelerationSpeed = SonicConsts.Physics.AirAccelerationSpeed;
  public float MaxFallSpeed = CommonConsts.Physics.MaxFallSpeed;
  public float GravityUpSpeed = CommonConsts.Physics.GravityUp;
  public float GravityDownSpeed = CommonConsts.Physics.GravityDown;
  public float GroundSpeedDeadZone = 0.5f;
  public float InputDeadZone = 0.001f;
  public bool GravityDownEnabled = true;

  [Header("Ground")]
  public LayerMask GroundLayer;

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
  };

  private void Awake()
  {
    Application.targetFrameRate = CommonConsts.ConvertValues.FramePerSec;
    Time.fixedDeltaTime = 1f / CommonConsts.ConvertValues.FramePerSec;

    _inputInfo = new InputInfo(
      () => Input.GetAxis(CommonConsts.InputAxis.Horizontal),
      () => Input.GetAxis(CommonConsts.InputAxis.Vertical));
    _playerSpeedManager = new PlayerSpeedManager(_inputInfo);
  }

  private void FixedUpdate()
  {
    UpdateInput();
    RunSensors();
    UpdateState();
    SetSpeed();
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

  private void UpdatePosition()
  {
    // Speed X, Y - offsets in units per frame.
    transform.position += new Vector3(_playerSpeedManager.SpeedX, _playerSpeedManager.SpeedY);
  }
}
