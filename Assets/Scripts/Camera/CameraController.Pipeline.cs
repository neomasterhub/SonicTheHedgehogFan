using UnityEngine;

/// <summary>
/// Pipeline.
/// </summary>
public partial class CameraController : MonoBehaviour
{
  private void FixedUpdate()
  {
    ApplyMovement();
  }

  private void ApplyMovement()
  {
    _cm.enabled = !_target.IsDying;
  }
}
