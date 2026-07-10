using UnityEngine;

/// <summary>
/// Debug.
/// </summary>
public partial class RingController : MonoBehaviour
{
  private void LateUpdate()
  {
    _sensorSystem.Draw();
  }
}
