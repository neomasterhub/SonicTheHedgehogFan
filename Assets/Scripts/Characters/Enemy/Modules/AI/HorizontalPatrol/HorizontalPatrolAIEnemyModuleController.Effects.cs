/// <summary>
/// Effects.
/// </summary>
public partial class HorizontalPatrolAIEnemyModuleController
{
  protected override void SetEffectPipeline()
  {
    _effects.AddStep(CreateEffect_Stop());
  }

  private PipelineStep CreateEffect_Stop()
  {
    return PipelineStepBuilder.Create()
      .WithDisplayName("Stop")
      .WithCondition(() =>
        !_context.IsStatic
        && (transform.position.x <= _minPositionX
        || transform.position.x >= _maxPositionX))
      .WithAction(() =>
      {
        _context.IsStatic = true;

        return PipelineStepResult.Break;
      })
      .Build();
  }
}
