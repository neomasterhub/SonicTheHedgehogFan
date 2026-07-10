using UnityEngine;

/// <summary>
/// Debug.
/// </summary>
public partial class RingController : MonoBehaviour
{
  private void DrawSensorSystem()
  {
    if (_debugMode)
    {
      _sensorSystem.Draw();
    }
  }
}
