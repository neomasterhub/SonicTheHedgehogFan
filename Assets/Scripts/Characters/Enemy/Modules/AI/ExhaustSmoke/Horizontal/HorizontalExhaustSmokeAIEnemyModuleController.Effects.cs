using UnityEngine;

/// <summary>
/// Effects.
/// </summary>
public partial class HorizontalExhaustSmokeAIEnemyModuleController
{
  protected override void SetEffectPipeline()
  {
    _effects.AddStep(CreateEffect_SetOrigin());
    _effects.AddStep(CreateEffect_UpdatePosition());
  }

  private PipelineStep CreateEffect_SetOrigin()
  {
    return PipelineStepBuilder.Create()
      .WithDisplayName("Set exhaust smoke origin")
      .WithCondition(() =>
        (_context.HorizontalDirection && _exhaustSmokeOrigin.x > 0)
        || (!_context.HorizontalDirection && _exhaustSmokeOrigin.x < 0))
      .WithAction(() =>
      {
        _exhaustSmokeOrigin = new Vector3(-_exhaustSmokeOrigin.x, _exhaustSmokeOrigin.y);

        return PipelineStepResult.Continue;
      })
      .Build();
  }

  private PipelineStep CreateEffect_UpdatePosition()
  {
    return PipelineStepBuilder.Create()
      .WithDisplayName("Update exhaust smoke position")
      .WithAction(() =>
      {
        var animatorState = _exhaustSmokeAnimator.GetCurrentAnimatorStateInfo(0);

        if (animatorState.normalizedTime > 1)
        {
          _exhaustSmokeAnimator.Play("ExhaustSmoke", 0, 0);
          _exhaustSmokePosition = transform.position + _exhaustSmokeOrigin;
        }

        _exhaustSmokeTransform.position = _exhaustSmokePosition;

        return PipelineStepResult.Continue;
      })
      .Build();
  }
}
