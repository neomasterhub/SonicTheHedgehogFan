/// <summary>
/// Effects.
/// </summary>
public partial class SonicController
{
  private void SetEffectPipeline()
  {
    _effects.AddStep(CreateEffect_Rolling_Exit());
    _effects.AddStep(CreateEffect_CurlingUp_Exit());
    _effects.AddStep(CreateEffect_CurlingUp_Enter());
    _effects.AddStep(CreateEffect_LookingUp_Exit());
  }

  private PipelineStep CreateEffect_Rolling_Exit()
  {
    return PipelineStepBuilder.Create()
      .WithDisplayName("Rolling/Exit")
      .WithCondition(() =>
        _isDownGrounded
        && _speedSystem.GroundSpeed == 0
        && _isRolling)
      .WithAction(() =>
      {
        _isRolling = false;
        _sizeMode = SonicSizeMode.Big;

        return PipelineStepResult.Continue;
      })
      .Build();
  }

  private PipelineStep CreateEffect_CurlingUp_Exit()
  {
    return PipelineStepBuilder.Create()
      .WithDisplayName("Curling Up/Exit")
      .WithCondition(() =>
        _isDownGrounded
        && _speedSystem.GroundSpeed == 0
        && !_isBalancing
        && _inputSystem.Released == PlayerInput.Down)
      .WithAction(() =>
      {
        _sizeMode = SonicSizeMode.Big;
        _isCurlingUp = false;

        return PipelineStepResult.Break;
      })
      .Build();
  }

  private PipelineStep CreateEffect_CurlingUp_Enter()
  {
    return PipelineStepBuilder.Create()
      .WithDisplayName("Curling Up/Enter")
      .WithCondition(() =>
        _isDownGrounded
        && _speedSystem.GroundSpeed == 0
        && !_isBalancing
        && _inputSystem.Held.HasAny(PlayerInput.Down))
      .WithAction(() =>
      {
        _sizeMode = SonicSizeMode.Small;
        _isCurlingUp = true;

        return PipelineStepResult.Break;
      })
      .Build();
  }

  private PipelineStep CreateEffect_LookingUp_Exit()
  {
    return PipelineStepBuilder.Create()
      .WithDisplayName("Looking Up/Exit")
      .WithCondition(() =>
        _isDownGrounded
        && _speedSystem.GroundSpeed == 0
        && !_isBalancing
        && _inputSystem.Released == PlayerInput.Up)
      .WithAction(() =>
      {
        _isLookingUp = false;

        return PipelineStepResult.Break;
      })
      .Build();
  }
}
