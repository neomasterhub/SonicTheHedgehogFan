using UnityEngine;

public class SonicController : MonoBehaviour
{
  private GroundSide _groundSide = GroundSide.Down;
  private SonicSensorSystem _sonicSensorSystem = new();
  private SonicSizeMode _sonicSizeMode = SonicSizeMode.Big;
  private SonicState _state = SonicState.None;
  private Vector2 _velocity;

  [Header("Physics")]
  public float GravityUp = 1f;
  public float GravityDown = 1f;
  public float MaxFallSpeed = 1f;

  [Header("Ground")]
  public LayerMask GroundLayer;

  [Header("UI")]
  public float GroundNormalLength = 1.5f;
  public float SensorLength = SonicConsts.Sensors.Length;
  public float SensorBeginRadius = 0.03f;
  public float SensorEndRadius = 0.01f;

  private void FixedUpdate()
  {
    RunSensors();
    UpdateState();
    UpdateGravity();
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
    _sonicSensorSystem.ApplyAB(GroundLayer);
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
    _velocity.y -= g * Time.deltaTime;

    if (_velocity.y < -MaxFallSpeed)
    {
      _velocity.y = -MaxFallSpeed;
    }
  }

  private void UpdatePosition()
  {
    transform.position += (Vector3)_velocity * Time.deltaTime;
  }
}
