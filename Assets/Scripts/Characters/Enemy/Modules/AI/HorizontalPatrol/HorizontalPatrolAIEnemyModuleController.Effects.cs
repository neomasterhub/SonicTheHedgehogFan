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
        !_isStopped
        && (transform.position.x <= _minPositionX
        || transform.position.x >= _maxPositionX))
      .WithAction(() =>
      {
        _isStopped = true;
        _speed = -_speed;
        _timerSystem.StartIfNotRunning(_stopTimer);
        _context.IsStatic = true;

        return PipelineStepResult.Break;
      })
      .Build();
  }
}
