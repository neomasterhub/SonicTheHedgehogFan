/// <summary>
/// Effects.
/// </summary>
public partial class EnemyController
{
  private void SetEffectPipeline()
  {
    _effects.AddStep(CreateEffect_Hit());
    _effects.AddStep(CreateEffect_GettingHit());
  }

  private PipelineStep CreateEffect_Hit()
  {
    return PipelineStepBuilder.Create()
      .WithDisplayName("Hit")
      .WithCondition(() =>
        _otherEnemy != null
        && !_otherEnemy.IsInvincible
        && !_otherEnemy.IsAttacking
        && !_otherEnemy.IsHurt
        && _collider.bounds.Intersects(_otherEnemyCollider.bounds))
      .WithAction(() =>
      {
        _otherEnemy.IsHit = true;
        _otherEnemy.IsHurt = true;
        _otherEnemy.LastHitSource = gameObject;

        return PipelineStepResult.Break;
      })
      .Build();
  }

  private PipelineStep CreateEffect_GettingHit()
  {
    return PipelineStepBuilder.Create()
      .WithDisplayName("Getting hit")
      .WithCondition(() =>
        _otherEnemy != null
        && !_otherEnemy.IsAttacking
        && _collider.bounds.Intersects(_otherEnemyCollider.bounds))
      .WithAction(() =>
      {
        _otherEnemy.LastHitSource = gameObject;

        return PipelineStepResult.Break;
      })
      .Build();
  }
}
