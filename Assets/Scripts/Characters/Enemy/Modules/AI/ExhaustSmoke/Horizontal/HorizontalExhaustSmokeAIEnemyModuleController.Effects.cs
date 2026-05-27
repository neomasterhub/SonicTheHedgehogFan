using UnityEngine;

/// <summary>
/// Effects.
/// </summary>
public partial class HorizontalExhaustSmokeAIEnemyModuleController
{
  protected override void SetEffectPipeline()
  {
    _effects.AddStep(CreateEffect_SetOrigin());
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
}
