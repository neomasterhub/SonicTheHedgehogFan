/// <summary>
/// Effects.
/// </summary>
public partial class EnemyController
{
  private void SetEffectPipeline()
  {
    _effects.AddStep(CreateEffect_Hit());
  }

  private PipelineStep CreateEffect_Hit()
  {
    return PipelineStepBuilder.Create()
      .WithDisplayName("Hit")
      .WithCondition(() => true)
      .WithAction(() =>
      {
        return PipelineStepResult.Break;
      })
      .Build();
  }
}
