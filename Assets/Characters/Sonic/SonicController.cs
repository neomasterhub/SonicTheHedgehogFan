using UnityEngine;

public class SonicController : MonoBehaviour
{
  private readonly SonicSensorSystem _sensorSystem = new();

  [Header("Sensors")]
  public Vector3 TopUDFLengths = new(0.3f, 0.3f, 0.5f);
  public Vector3 BottomUDFLengths = new(0.3f, 0.1f, 0.5f);

  private void OnDrawGizmos()
  {
    _sensorSystem.CurrentSensorGroup.Draw();
  }
}
