using UnityEngine;

public class SonicController : MonoBehaviour
{
  private GroundSide _groundSide = GroundSide.Down;
  private SonicSensorSystem _sonicSensorSystem = new();
  private SonicSizeMode _sonicSizeMode = SonicSizeMode.Big;
  private SonicState _state = SonicState.None;

  /// <summary>
  /// Offset in units per frame.
  /// </summary>
  private Vector2 _velocity;

  [Header("Physics")]
  public float GravityUp = CommonConsts.Physics.GravityUp;
  public float GravityDown = CommonConsts.Physics.GravityDown;
  public float MaxFallSpeed = CommonConsts.Physics.MaxFallSpeed;
  public float AccelerationSpeed = SonicConsts.Physics.AccelerationSpeed;
  public float DecelerationSpeed = SonicConsts.Physics.AccelerationSpeed;
  public float TopSpeed = SonicConsts.Physics.TopSpeed;

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
    RunSensors();
    UpdateState();
    UpdateGravity();
    PreventGroundOvershoot();
    UpdatePosition();
  }

  private void OnDrawGizmos()
  {
    _sonicSensorSystem.DrawSensors(SensorBeginRadius, SensorEndRadius);
    _sonicSensorSystem.DrawGroundNormal(GroundNormalLength, SensorBeginRadius, SensorEndRadius);
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
      if (_velocity.y < 0)
      {
        _velocity.y = 0;
      }

      return;
    }

    var g = _velocity.y > 0 ? GravityUp : GravityDown;
    _velocity.y -= g;

    if (_velocity.y < -MaxFallSpeed)
    {
      _velocity.y = -MaxFallSpeed;
    }
  }

  private void PreventGroundOvershoot()
  {
    if (_velocity.y >= 0)
    {
      return;
    }

    // Keeps surface normal aligned with slope.
    var yPositionOffset = SensorLength / 2;

    _velocity.y = -Mathf.Min(Mathf.Abs(_velocity.y), _sonicSensorSystem.ABResult.Distance - yPositionOffset);
  }

  private void UpdatePosition()
  {
    transform.position += (Vector3)_velocity;
  }
}
