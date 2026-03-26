using UnityEngine;

public class SonicController : MonoBehaviour
{
  private readonly SonicSensorSystem _sensorSystem = new();

  // Flags
  private SonicSizeMode _sizeMode;
  private GroundSide _groundSide;

  [Header("Sensors")]
  public Vector3 TopUDFLengths = new(0.3f, 0.3f, 0.5f);
  public Vector3 BottomUDFLengths = new(0.3f, 0.1f, 0.5f);

  private void OnDrawGizmos()
  {
    _sensorSystem.CurrentSensorGroup.Draw();
  }

  private void FixedUpdate()
  {
    UpdateSensors();
  }

  private void UpdateSensors()
  {
    _sensorSystem.Update(_sizeMode, _groundSide, transform.position);
  }
}
