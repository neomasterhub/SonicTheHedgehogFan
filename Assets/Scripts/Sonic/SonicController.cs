using SonicTheHedgehogFan.Engine;
using UnityEngine;

public class SonicController : MonoBehaviour
{
  private readonly InputInfo _inputInfo = new(
    () => Input.GetAxis(CommonConsts.InputAxis.Horizontal),
    () => Input.GetAxis(CommonConsts.InputAxis.Vertical));
  private readonly SonicSensorSystem _sonicSensorSystem = new();

  private float _groundSpeed;
  private GroundSide _groundSide = GroundSide.Down;
  private SonicSizeMode _sonicSizeMode = SonicSizeMode.Big;
  private SonicState _state = SonicState.None;

  /// <summary>
  /// Offset in units per frame.
  /// </summary>
  private Vector2 _speed;

  [Header("Physics")]
  public float GravityUp = CommonConsts.Physics.GravityUp;
  public float GravityDown = CommonConsts.Physics.GravityDown;
  public float MaxFallSpeed = CommonConsts.Physics.MaxFallSpeed;
  public float AccelerationSpeed = SonicConsts.Physics.AccelerationSpeed;
  public float DecelerationSpeed = SonicConsts.Physics.AccelerationSpeed;
  public float FrictionSpeed = SonicConsts.Physics.FrictionSpeed;
  public float TopSpeed = SonicConsts.Physics.TopSpeed;
  public float GroundSpeedDead = 0.5f;

  [Header("Ground")]
  public LayerMask GroundLayer;

  [Header("UI")]
  public float GroundNormalLength = 1.5f;
  public float SensorLength = SonicConsts.Sensors.Length;
  public float SensorBeginRadius = 0.03f;
  public float SensorEndRadius = 0.01f;

  private void Awake()
  {
    Application.targetFrameRate = CommonConsts.ConvertValues.FramePerSec;
    Time.fixedDeltaTime = 1f / CommonConsts.ConvertValues.FramePerSec;
  }

  private void FixedUpdate()
  {
    UpdateInput();

    RunSensors();
    UpdateState();
    // Set specific speed.
    UpdateGravity();
    PreventGroundOvershoot();
    UpdateGroundSpeed();

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
    var state = SonicState.None;

    if (_sonicSensorSystem.ABResult.GroundDetected)
    {
      state |= SonicState.Grounded;
    }

    Debug.Log(state);

    _state = state;
  }

  private void UpdateGravity()
  {
    if (_state.HasFlag(SonicState.Grounded))
    {
      if (_speed.y < 0)
      {
        _speed.y = 0;
      }

      return;
    }

    var g = _speed.y > 0 ? GravityUp : GravityDown;
    _speed.y -= g;

    if (_speed.y < -MaxFallSpeed)
    {
      _speed.y = -MaxFallSpeed;
    }
  }

  private void PreventGroundOvershoot()
  {
    if (_speed.y >= 0)
    {
      return;
    }

    // Keeps surface normal aligned with slope.
    var yPositionOffset = SensorLength / 2;

    _speed.y = -Mathf.Min(Mathf.Abs(_speed.y), _sonicSensorSystem.ABResult.Distance - yPositionOffset);
  }

  private void UpdateGroundSpeed()
  {
    if (_inputInfo.XDirection > 0)
    {
      SetForwardGroundSpeed();
      return;
    }

    if (_inputInfo.XDirection < 0)
    {
      SetBackGroundSpeed();
      return;
    }

    _groundSpeed = Mathf.Abs(_groundSpeed) > GroundSpeedDead
      ? _groundSpeed - (FrictionSpeed * _inputInfo.XDirection)
      : 0;
  }

  private void SetForwardGroundSpeed()
  {
    if (_groundSpeed < 0)
    {
      _groundSpeed += DecelerationSpeed;

      if (_groundSpeed >= 0)
      {
        _groundSpeed = DecelerationSpeed;
      }
    }
    else if (_groundSpeed < TopSpeed)
    {
      _groundSpeed += AccelerationSpeed;

      if (_groundSpeed >= TopSpeed)
      {
        _groundSpeed = TopSpeed;
      }
    }
  }

  private void SetBackGroundSpeed()
  {
    if (_groundSpeed > 0)
    {
      _groundSpeed -= DecelerationSpeed;

      if (_groundSpeed <= 0)
      {
        _groundSpeed = -DecelerationSpeed;
      }
    }
    else if (_groundSpeed > -TopSpeed)
    {
      _groundSpeed -= AccelerationSpeed;

      if (_groundSpeed <= -TopSpeed)
      {
        _groundSpeed = -TopSpeed;
      }
    }
  }

  private void UpdatePosition()
  {
    // TODO: UpdateSpeed()
    _speed.x = _groundSpeed * Mathf.Cos(_sonicSensorSystem.ABResult.AngleRad);
    _speed.y = _groundSpeed * Mathf.Sin(_sonicSensorSystem.ABResult.AngleRad);
    transform.position += (Vector3)_speed;
  }
}
