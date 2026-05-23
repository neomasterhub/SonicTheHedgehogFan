/// <summary>
/// Effects.
/// </summary>
public partial class EnemyController
{
  private void SetEffectPipeline()
  {
    _effects.AddStep(CreateEffect_Intersected());
    _effects.AddStep(CreateEffect_GettingHit());
  }

  private PipelineStep CreateEffect_Intersected()
  {
    return PipelineStepBuilder.Create()
      .WithDisplayName("Intersected")
      .WithAction(() =>
      {
        return _otherEnemy == null
          || !_collider.bounds.Intersects(_otherEnemyCollider.bounds)
          ? PipelineStepResult.Break
          : PipelineStepResult.Continue;
      })
      .Build();
  }

  private PipelineStep CreateEffect_GettingHit()
  {
    return PipelineStepBuilder.Create()
      .WithDisplayName("Getting hit")
      .WithCondition(() =>
        _isAlive
        && !IsHurt
        && !IsInvincible
        && _otherEnemy.IsAttacking)
      .WithAction(() =>
      {
        IsHit = true;
        IsHurt = true;

        _otherEnemy.ContactEnemy = this;

        return PipelineStepResult.Break;
      })
      .Build();
  }
}
