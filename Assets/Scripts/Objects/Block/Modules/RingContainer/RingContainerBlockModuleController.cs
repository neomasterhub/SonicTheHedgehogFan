using UnityEngine;

/// <summary>
/// Data.
/// </summary>
public partial class RingContainerBlockModuleController
  : BlockModuleControllerBase
{
  private IRingCollector _ringCollector;

  [SerializeField]
  private int _ringCount;
}
