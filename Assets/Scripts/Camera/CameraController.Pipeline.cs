using UnityEngine;

/// <summary>
/// Pipeline.
/// </summary>
public partial class CameraController : MonoBehaviour
{
  private void FixedUpdate()
  {
    ApplyEffects();
    ApplyMovement();
  }

  private void ApplyEffects()
  {
    _effects.Run(false);
  }

  private void ApplyMovement()
  {
    _cm.enabled = !_target.IsDying;
  }
}
