/// <summary>
/// Effects.
/// </summary>
public partial class BlockController
{
  private void SetEffectPipeline()
  {
    _effects.AddStep(CreateEffect_Intersect());
    _effects.AddStep(CreateEffect_Contact());
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

  private PipelineStep CreateEffect_Contact()
  {
    return PipelineStepBuilder.Create()
      .WithDisplayName("Contact")
      .WithAction(() =>
      {
        _player.ContactBlock = this;

        return PipelineStepResult.Continue;
      })
      .Build();
  }
}
