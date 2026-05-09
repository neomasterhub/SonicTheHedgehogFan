using UnityEngine;

/// <summary>
/// Debug.
/// </summary>
public partial class RingController : MonoBehaviour
{
  private void OnDrawGizmos()
  {
    _sensorSystem.Draw();
  }
}
