public abstract class AIEnemyModuleControllerBase
  : EnemyModuleControllerBase
{
  private readonly Pipeline _effects;

  protected AIEnemyModuleControllerBase()
  {
    _effects = new();
    SetEffectPipeline();
  }

  protected abstract void SetEffectPipeline();

  public override void Apply()
  {
    _effects.Run(false);
  }
}
