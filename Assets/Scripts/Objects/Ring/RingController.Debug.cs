using UnityEngine;
using static RingConsts.Physics;
using static SharedConsts.Colors;
using static SharedConsts.Physics.SensorRayLengths;

/// <summary>
/// Debug.
/// </summary>
public partial class RingController : MonoBehaviour
{
  private void DrawSensorSystem()
  {
    if (_debugMode)
    {
      DrawSensorSystemBG();
      _sensorSystem.Draw();
    }
  }

  private void DrawSensorSystemBG()
  {
    var origin = transform.position + new Vector3(0, SensorY);

    _meshRenderer.DrawLine(
      origin + new Vector3(0, GroundInner),
      origin - new Vector3(0, GroundOuter),
      0.3f,
      SensorSystemBG);
  }
}
