using UnityEngine;

[ExecuteAlways]
public class SonicController : MonoBehaviour
{
  private GroundSide _groundSide = GroundSide.Down;
  private SonicSizeMode _sonicSizeMode = SonicSizeMode.Big;
  private SonicSensorSystem _sonicSensorSystem = new();

  [Header("UI")]
  public float SensorLength = SonicConsts.Sensors.Length;
  public float SensorBeginRadius = 0.03f;
  public float SensorEndRadius = 0.01f;

  private void Update()
  {
    UpdateSensors();
  }

  private void OnDrawGizmos()
  {
    _sonicSensorSystem.Draw(SensorBeginRadius, SensorEndRadius);
  }

  private void UpdateSensors()
  {
    _sonicSensorSystem.Update(transform.position, _sonicSizeMode, _groundSide, SensorLength);
  }
}
