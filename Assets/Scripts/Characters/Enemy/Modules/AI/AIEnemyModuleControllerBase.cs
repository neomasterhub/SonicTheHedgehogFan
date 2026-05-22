public abstract class AIEnemyModuleControllerBase
  : EnemyModuleControllerBase
{
  protected readonly Pipeline _effects;

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
