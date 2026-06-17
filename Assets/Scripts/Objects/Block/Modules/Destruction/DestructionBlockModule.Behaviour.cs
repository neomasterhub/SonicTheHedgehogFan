using static SharedConsts.Physics;

/// <summary>
/// Behaviour.
/// </summary>
public partial class DestructionBlockModule
{
  public override void Apply()
  {
    ToggleBlockLayer();
  }

  private void ToggleBlockLayer()
  {
    gameObject.layer = BlockLayerIndex;
  }
}
