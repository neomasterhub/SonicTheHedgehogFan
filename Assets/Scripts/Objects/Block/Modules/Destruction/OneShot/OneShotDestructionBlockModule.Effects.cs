/// <summary>
/// Effects.
/// </summary>
public partial class OneShotDestructionBlockModule
{
  private void SetEffectPipeline()
  {
    _effects.AddStep(CreateEffect_Attacked());
  }

  private PipelineStep CreateEffect_Attacked()
  {
    return PipelineStepBuilder.Create()
      .WithDisplayName("Attacked")
      .WithCondition(() =>
        _playerIsAttacking
        && _playerIsIntersecting
        && !_context.IsHurt)
      .WithAction(() =>
      {
        _context.IsHit = true;
        _context.IsHurt = true;
        _context.Health = -1;

        return PipelineStepResult.Continue;
      })
      .Build();
  }
}
