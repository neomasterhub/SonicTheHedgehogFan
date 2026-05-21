/// <summary>
/// Effects.
/// </summary>
public partial class EnemyController
{
  private void SetEffectPipeline()
  {
    _effects.AddStep(CreateEffect_Intersected());
    _effects.AddStep(CreateEffect_Hit());
    _effects.AddStep(CreateEffect_GettingHit());
  }

  private PipelineStep CreateEffect_Intersected()
  {
    return PipelineStepBuilder.Create()
      .WithDisplayName("Intersected")
      .WithAction(() =>
      {
        _otherEnemy.ContactEnemy = null;

        return _otherEnemy == null
          || !_collider.bounds.Intersects(_otherEnemyCollider.bounds)
          ? PipelineStepResult.Break
          : PipelineStepResult.Continue;
      })
      .Build();
  }

  private PipelineStep CreateEffect_Hit()
  {
    return PipelineStepBuilder.Create()
      .WithDisplayName("Hit")
      .WithCondition(() =>
        _isAlive
        && !_otherEnemy.IsInvincible
        && !_otherEnemy.IsAttacking
        && !_otherEnemy.IsHurt)
      .WithAction(() =>
      {
        _otherEnemy.IsHit = true;
        _otherEnemy.IsHurt = true;
        _otherEnemy.ContactEnemy = this;

        return PipelineStepResult.Break;
      })
      .Build();
  }

  private PipelineStep CreateEffect_GettingHit()
  {
    return PipelineStepBuilder.Create()
      .WithDisplayName("Getting hit")
      .WithCondition(() =>
        _isAlive
        && _otherEnemy.IsAttacking)
      .WithAction(() =>
      {
        _isAlive = false;
        _timerSystem.StartIfNotRunning(_deadActiveTimer);
        _otherEnemy.ContactEnemy = this;

        return PipelineStepResult.Break;
      })
      .Build();
  }
}
