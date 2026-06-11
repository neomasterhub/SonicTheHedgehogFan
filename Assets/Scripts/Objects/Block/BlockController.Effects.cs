/// <summary>
/// Effects.
/// </summary>
public partial class BlockController
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
        return _collider.bounds.Intersects(_playerCollider.bounds)
          ? PipelineStepResult.Continue
          : PipelineStepResult.Break;
      })
      .Build();
  }
}
