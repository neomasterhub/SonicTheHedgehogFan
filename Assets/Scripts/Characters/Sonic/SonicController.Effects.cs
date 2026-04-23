/// <summary>
/// Effects.
/// </summary>
public partial class SonicController
{
  private void SetEffectPipeline()
  {
    _effects.AddStep(CreateEffect_Rolling_Exit());
    _effects.AddStep(CreateEffect_CurlingUp_Exit());
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
}
