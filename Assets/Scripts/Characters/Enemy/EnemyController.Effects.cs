/// <summary>
/// Effects.
/// </summary>
public partial class EnemyController
{
  private void SetEffectPipeline()
  {
    _effects.AddStep(CreateEffect_Intersect());
    _effects.AddStep(CreateEffect_Contact());
    _effects.AddStep(CreateEffect_Attacked());
  }

  private PipelineStep CreateEffect_Intersect()
  {
    return PipelineStepBuilder.Create()
      .WithDisplayName("Intersect")
      .WithAction(() =>
      {
        return _otherEnemy != null
          && _collider.bounds.Intersects(_otherEnemyCollider.bounds)
          ? PipelineStepResult.Continue
          : PipelineStepResult.Break;
      })
      .Build();
  }

  private PipelineStep CreateEffect_Contact()
  {
    return PipelineStepBuilder.Create()
      .WithDisplayName("Contact")
      .WithAction(() =>
      {
        if (_isAlive
          && !IsHurt
          && !IsInvincible)
        {
          _otherEnemy.ContactEnemy = this;

          return PipelineStepResult.Continue;
        }

        return PipelineStepResult.Break;
      })
      .Build();
  }

  private PipelineStep CreateEffect_Attacked()
  {
    return PipelineStepBuilder.Create()
      .WithDisplayName("Attacked")
      .WithCondition(() =>
        _otherEnemy.IsAttacking)
      .WithAction(() =>
      {
        IsHit = true;
        IsHurt = true;
        IsInvincible = true;
        Health--;

        return PipelineStepResult.Continue;
      })
      .Build();
  }
}
