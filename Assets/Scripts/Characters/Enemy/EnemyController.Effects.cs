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
      .WithCondition(() =>
        _otherEnemy != null
        && _collider.bounds.Intersects(_otherEnemyCollider.bounds))
      .WithAction(() =>
      {
        if (_otherEnemy == null
          || !_collider.bounds.Intersects(_otherEnemyCollider.bounds))
        {
          return PipelineStepResult.Break;
        }

        _otherEnemy.LastHitSource = gameObject;

        return PipelineStepResult.Continue;
      })
      .Build();
  }

  private PipelineStep CreateEffect_Hit()
  {
    return PipelineStepBuilder.Create()
      .WithDisplayName("Hit")
      .WithCondition(() =>
        !_otherEnemy.IsInvincible
        && !_otherEnemy.IsAttacking
        && !_otherEnemy.IsHurt)
      .WithAction(() =>
      {
        _otherEnemy.IsHit = true;
        _otherEnemy.IsHurt = true;

        return PipelineStepResult.Break;
      })
      .Build();
  }

  private PipelineStep CreateEffect_GettingHit()
  {
    return PipelineStepBuilder.Create()
      .WithDisplayName("Getting hit")
      .WithCondition(() =>
        _otherEnemy.IsAttacking)
      .WithAction(() =>
      {
        return PipelineStepResult.Break;
      })
      .Build();
  }
}
