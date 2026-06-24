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
        && !_context.IsHurt
        && !_context.IsPushedUpIntersecting)
      .WithAction(() =>
      {
        _context.IsHit = true;
        _context.IsHurt = true;
        _context.Health = -1;

        _player.ReboundSignal = new(ReboundSourceType.Block, _context.Health, _context.SpeedX, _context.SpeedY);

        gameObject.SetActive(false);

        return PipelineStepResult.Continue;
      })
      .Build();
  }
}
