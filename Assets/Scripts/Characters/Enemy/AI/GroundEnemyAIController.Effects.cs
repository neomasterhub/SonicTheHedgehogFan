/// <summary>
/// Effects.
/// </summary>
public partial class GroundEnemyAIController
{
  private void SetEffectPipeline()
  {
    Effects.AddStep(CreateEffect_Stop());
  }

  private PipelineStep CreateEffect_Stop()
  {
    return PipelineStepBuilder.Create()
      .WithDisplayName("Stop")
      .WithCondition(() =>
        transform.position.x <= _minPositionX
        || transform.position.x >= _maxPositionX)
      .WithAction(() =>
      {
        return PipelineStepResult.Break;
      })
      .Build();
  }
}
