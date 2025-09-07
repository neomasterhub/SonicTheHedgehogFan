using UnityEngine;

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
  public float GroundNormalLength = 1.5f;
  public float SensorLength = SonicConsts.Sensors.Length;
  public float SensorBeginRadius = 0.03f;
  public float SensorEndRadius = 0.01f;

  private void Update()
  {
    RunSensors();
    UpdateState();
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

  private void UpdatePosition()
  {
    transform.position += (Vector3)_velocity * Time.deltaTime;
  }
}
