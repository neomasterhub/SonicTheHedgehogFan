using UnityEngine;
using static BlockConsts;
using static SharedConsts;

/// <summary>
/// Init.
/// </summary>
public partial class RingContainerBlockModuleController
{
  public RingContainerBlockModuleController()
    : base()
  {
    _ringCount = MonitorRingCount;
  }

  public override void Initialize(BlockControllerBase context)
  {
    base.Initialize(context);
    _ringCollector = GameObject.FindWithTag(Tags.Player).GetComponent<IRingCollector>();
  }
}
