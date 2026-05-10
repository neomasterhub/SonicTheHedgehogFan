using static RingConsts.UI;
using AnimatorParameters = SharedConsts.Animator.Parameters;

/// <summary>
/// Effects.
/// </summary>
public partial class RingController
{
  private void SetEffectPipeline()
  {
    _effects.AddStep(CreateEffect_CollectByPlayer());
  }

  private PipelineStep CreateEffect_CollectByPlayer()
  {
    return PipelineStepBuilder.Create()
      .WithDisplayName("Collect by player")
      .WithCondition(() =>
        _player != null
        && _collider.bounds.Intersects(_playerCollider.bounds))
      .WithAction(() =>
      {
        _isCollected = true;
        _playerRings.Add();
        _animator.SetTrigger(AnimatorParameters.Collected);
        _spriteRenderer.sortingOrder = SparkleOrderInLayer;

        return PipelineStepResult.Break;
      })
      .Build();
  }
}
