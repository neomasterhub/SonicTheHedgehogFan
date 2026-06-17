/// <summary>
/// Init.
/// </summary>
public partial class DestructionBlockModule
  : BlockModuleControllerBase
{
  public override void Initialize(BlockControllerBase context)
  {
    base.Initialize(context);
    _player = context.Player;
  }
}
