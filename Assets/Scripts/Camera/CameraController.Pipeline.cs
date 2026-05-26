using UnityEngine;

/// <summary>
/// Pipeline.
/// </summary>
public partial class CameraController : MonoBehaviour
{
  private void FixedUpdate()
  {
    ApplyEffects();
  }

  private void ApplyEffects()
  {
    _effects.Run();
  }
}
