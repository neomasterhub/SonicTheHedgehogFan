/// <summary>
/// Effects.
/// </summary>
public partial class DestructionBlockModule
{
  private void SetEffectPipeline()
  {
    _effects.AddStep(CreateEffect_Intersect());
    _effects.AddStep(CreateEffect_Attacked());
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

  private PipelineStep CreateEffect_Attacked()
  {
    return PipelineStepBuilder.Create()
      .WithDisplayName("Attacked")
      .WithAction(() =>
      {
        _context.IsHit = true;
        _context.IsHurt = true;
        _context.Health--;

        return PipelineStepResult.Continue;
      })
      .Build();
  }
}
