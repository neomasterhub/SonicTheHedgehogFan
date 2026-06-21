/// <summary>
/// Init.
/// </summary>
public partial class OneShotDestructionBlockModule
{
  public OneShotDestructionBlockModule()
    : base()
  {
    _effects = new();
    SetEffectPipeline();
  }

  public override void Initialize(BlockControllerBase context)
  {
    base.Initialize(context);

    _player = context.Player;
  }
}
