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
        MustStop())
      .WithAction(() =>
      {
        _isStopped = true;
        _speed = -_speed;
        _timerSystem.StartIfNotRunning(_stopTimer);

        _context.IsStatic = true;
        _context.AccelerationSpeed = 0;

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
        _context.AccelerationSpeed = _speed;

        return PipelineStepResult.Break;
      })
      .Build();
  }

  private bool MustStop()
  {
    if (_isStopped)
    {
      return false;
    }

    if (_context.PositionX <= _minPositionX
      || _context.PositionX >= _maxPositionX)
    {
      return true;
    }

    var isGrounded = _context.Ground.HasValue;

    return (_context.LeftWall.HasValue && MustStop_LeftWall(_context.LeftWall.Value, isGrounded))
      || (_context.RightWall.HasValue && MustStop_RightWall(_context.RightWall.Value, isGrounded));
  }

  private bool MustStop_LeftWall(WallDetectionResult wall, bool isGrounded)
  {
    return MustStop_Wall(wall, isGrounded, _context.SpeedX <= -wall.Distance + _context.WallClearance);
  }

  private bool MustStop_RightWall(WallDetectionResult wall, bool isGrounded)
  {
    return MustStop_Wall(wall, isGrounded, _context.SpeedX >= wall.Distance - _context.WallClearance);
  }

  private bool MustStop_Wall(WallDetectionResult wall, bool isGrounded, bool speedCondition)
  {
    if (isGrounded && wall.AngleDeg != 0)
    {
      return false;
    }

    return speedCondition;
  }
}
