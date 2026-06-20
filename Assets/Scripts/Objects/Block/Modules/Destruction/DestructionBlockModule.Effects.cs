/// <summary>
/// Effects.
/// </summary>
public partial class DestructionBlockModule
{
  private void SetEffectPipeline()
  {
    _effects.AddStep(CreateEffect_Intersect());
  }

  private PipelineStep CreateEffect_Intersect()
  {
    return PipelineStepBuilder.Create()
      .WithDisplayName("Intersect")
      .WithAction(() =>
      {
        return _layer == 0
          && _collider.bounds.Intersects(_playerCollider.bounds)
          ? PipelineStepResult.Continue
          : PipelineStepResult.Break;
      })
      .Build();
  }
}
