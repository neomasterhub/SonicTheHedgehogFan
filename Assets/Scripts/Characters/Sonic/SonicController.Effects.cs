/// <summary>
/// Effects.
/// </summary>
public partial class SonicController
{
  private void SetEffectPipeline()
  {
    _effects.AddStep(CreateEffect_Rolling_Exit());
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
}
