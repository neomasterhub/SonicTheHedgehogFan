using UnityEngine;

public class SonicController : MonoBehaviour
{
  private readonly SonicSensorSystem _sensorSystem = new();

  public SonicController()
  {
    // For drawing
    _sensorSystem.SetCurrentSensorGroup(default, default);
  }

  private void OnDrawGizmos()
  {
    _sensorSystem.CurrentSensorGroup.Draw();
  }
}
