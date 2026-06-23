using UnityEngine;

/// <summary>
/// Data.
/// </summary>
public partial class GroundSpeedBlockModuleController
  : BlockModuleControllerBase
{
  private float _gravitySpeed;
  private float _maxFallSpeed;

  [SerializeField]
  private float _gravitySpeedSpx;
  [SerializeField]
  private float _maxFallSpeedPx;
}
