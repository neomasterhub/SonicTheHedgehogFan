/// <summary>
/// Effects.
/// </summary>
public partial class HorizontalPatrolAIEnemyModuleController
{
  protected override void SetEffectPipeline()
  {
    _effects.AddStep(CreateEffect_Stop());
    _effects.AddStep(CreateEffect_Move());
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
        _context.Speed = 0;

        return PipelineStepResult.Break;
      })
      .Build();
  }

  private PipelineStep CreateEffect_Move()
  {
    return PipelineStepBuilder.Create()
      .WithDisplayName("Move")
      .WithCondition(() =>
        !_isStopped
        && _context.IsStatic)
      .WithAction(() =>
      {
        _context.IsStatic = false;
        _context.Speed = _speed;

        return PipelineStepResult.Break;
      })
      .Build();
  }
}
