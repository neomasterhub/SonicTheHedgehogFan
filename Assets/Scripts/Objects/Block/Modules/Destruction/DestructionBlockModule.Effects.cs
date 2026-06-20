using UnityEngine;

/// <summary>
/// Effects.
/// </summary>
public partial class DestructionBlockModule
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
        && !_context.IsHit)
      .WithAction(() =>
      {
        _context.IsHit = true;
        _context.IsHurt = true;
        _context.Health--;

        _player.ReboundSpeed = new Vector2(_player.SpeedX, -_player.SpeedY);

        return PipelineStepResult.Continue;
      })
      .Build();
  }
}
