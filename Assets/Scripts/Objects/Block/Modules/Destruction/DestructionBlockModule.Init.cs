/// <summary>
/// Init.
/// </summary>
public partial class DestructionBlockModule
  : BlockModuleControllerBase
{
  public DestructionBlockModule()
    : base()
  {
    _effects = new();
    SetEffectPipeline();
  }

  public override void Initialize(BlockControllerBase context)
  {
    base.Initialize(context);
    _player = context.Player;
    _layer = gameObject.layer;
  }
}
