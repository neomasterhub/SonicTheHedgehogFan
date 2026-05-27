using UnityEngine;

/// <summary>
/// Effects.
/// </summary>
public partial class HorizontalExhaustSmokeAIEnemyModuleController
{
  protected override void SetEffectPipeline()
  {
    _effects.AddStep(CreateEffect_Read());
    _effects.AddStep(CreateEffect_SetOrigin());
  }

  private PipelineStep CreateEffect_Read()
  {
    return PipelineStepBuilder.Create()
      .WithDisplayName("Read exhaust smoke")
      .WithAction(() =>
      {
        _origin = _exhaustSmoke.Origin;

        return PipelineStepResult.Continue;
      })
      .Build();
  }

  private PipelineStep CreateEffect_SetOrigin()
  {
    return PipelineStepBuilder.Create()
      .WithDisplayName("Set exhaust smoke origin")
      .WithCondition(() =>
        (_context.HorizontalDirection && _origin.x < 0)
        || (!_context.HorizontalDirection && _origin.x > 0))
      .WithAction(() =>
      {
        _exhaustSmoke.Origin = new Vector3(-_origin.x, _origin.y);

        return PipelineStepResult.Continue;
      })
      .Build();
  }
}
