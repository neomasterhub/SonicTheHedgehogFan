using UnityEngine;

[ExecuteAlways]
public class SonicController : MonoBehaviour
{
  private GroundSide _groundSide = GroundSide.Down;
  private SonicSensorSystem _sonicSensorSystem = new();
  private SonicSizeMode _sonicSizeMode = SonicSizeMode.Big;
  private SonicState _state = SonicState.None;
  private Vector2 _velocity;

  [Header("Ground")]
  public LayerMask GroundLayer;

  [Header("UI")]
  public float SensorLength = SonicConsts.Sensors.Length;
  public float SensorBeginRadius = 0.03f;
  public float SensorEndRadius = 0.01f;

  private void Update()
  {
    UpdateSensors();
    UpdateState();
    UpdatePosition();
  }

  private void OnDrawGizmos()
  {
    _sonicSensorSystem.Draw(SensorBeginRadius, SensorEndRadius);
  }

  private void UpdateSensors()
  {
    _sonicSensorSystem.Update(transform.position, _sonicSizeMode, _groundSide, SensorLength);
  }

  private void UpdateState()
  {
    var state = SonicState.None;

    if (IsGrounded())
    {
      state |= SonicState.Grounded;
    }

    _state = state;
  }

  private void UpdatePosition()
  {
    transform.position += (Vector3)(_velocity * Time.deltaTime);
  }

  private bool IsGrounded()
  {
    var a = _sonicSensorSystem.Sensors[SensorId.A];
    var b = _sonicSensorSystem.Sensors[SensorId.B];

    return Physics2D.Raycast(a.Begin, a.Direction, a.Length, GroundLayer)
      || Physics2D.Raycast(b.Begin, b.Direction, b.Length, GroundLayer);
  }
}
