/// <summary>
/// Effects.
/// </summary>
public partial class DeathDefeatAIEnemyModuleController
{
  protected override void SetEffectPipeline()
  {
    _effects.AddStep(CreateEffect_Dying());
  }

  private PipelineStep CreateEffect_Dying()
  {
    return PipelineStepBuilder.Create()
      .WithDisplayName("Dying")
      .WithCondition(() =>
        _context.IsHit
        && _context.Health < 0)
      .WithAction(() =>
      {
        _context.IsHurt = false;
        _context.IsDying = true;
        _context.IsStatic = true;
        _context.Speed = 0;

        _timerSystem.StartIfNotRunning(_dyingTimer);

        return PipelineStepResult.Break;
      })
      .Build();
  }
}
