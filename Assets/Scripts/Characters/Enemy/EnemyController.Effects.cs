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
      .WithCondition(() =>
        _otherEnemy != null
        && !_otherEnemy.IsHit
        && !_otherEnemy.IsImmortal
        && !_otherEnemy.IsAttacking
        && _collider.bounds.Intersects(_otherEnemyCollider.bounds))
      .WithAction(() =>
      {
        _otherEnemy.IsHit = true;
        _otherEnemy.IsHurt = true;

        return PipelineStepResult.Break;
      })
      .Build();
  }
}
