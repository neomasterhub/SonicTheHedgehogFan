using UnityEngine;

public abstract class AIEnemyModuleControllerBase
  : EnemyModuleControllerBase
{
  protected readonly Pipeline _effects;
  protected readonly TimerSystem _timerSystem;

  protected AIEnemyModuleControllerBase()
    : base()
  {
    _effects = new();
    _timerSystem = new();
    SetEffectPipeline();
  }

  protected abstract void SetEffectPipeline();

  public override void Apply()
  {
    _timerSystem.Update(Time.deltaTime);
    _effects.Run();
  }
}
