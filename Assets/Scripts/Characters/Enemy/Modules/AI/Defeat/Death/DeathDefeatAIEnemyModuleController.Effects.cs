/// <summary>
/// Effects.
/// </summary>
public partial class DeathDefeatAIEnemyModuleController
{
  protected override void SetEffectPipeline()
  {
    _effects.AddStep(CreateEffect_Death());
  }

  private PipelineStep CreateEffect_Death()
  {
    return PipelineStepBuilder.Create()
      .WithDisplayName("Death")
      .WithCondition(() =>
        _context.Health < 0
        && !_context.IsDead)
      .WithAction(() =>
      {
        _context.IsDead = true;
        _context.IsStatic = true;
        _context.Speed = 0;

        return PipelineStepResult.Break;
      })
      .Build();
  }
}
