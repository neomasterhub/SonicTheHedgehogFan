using static RingConsts.UI;
using AnimatorParameters = SharedConsts.Animator.Parameters;

/// <summary>
/// Effects.
/// </summary>
public partial class RingController
{
  private void SetEffectPipeline()
  {
    _effects.AddStep(CreateEffect_Destroy());
    _effects.AddStep(CreateEffect_CollectByPlayer());
  }

  private PipelineStep CreateEffect_Destroy()
  {
    return PipelineStepBuilder.Create()
      .WithDisplayName("Destroy")
      .WithCondition(() =>
        _lifetime <= 0)
      .WithAction(() =>
      {
        Destroy(transform.gameObject);

        return PipelineStepResult.Break;
      })
      .Build();
  }

  private PipelineStep CreateEffect_CollectByPlayer()
  {
    return PipelineStepBuilder.Create()
      .WithDisplayName("Collect by player")
      .WithCondition(() =>
        !_isCollected
        && _player != null
        && _collider.bounds.Intersects(_playerCollider.bounds))
      .WithAction(() =>
      {
        _lifetime = 1;
        _isCollected = true;
        _playerRings.Add();
        _animator.SetTrigger(AnimatorParameters.Collected);
        _spriteRenderer.sortingOrder = SparkleOrderInLayer;

        return PipelineStepResult.Break;
      })
      .Build();
  }
}
