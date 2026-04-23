/// <summary>
/// Effects.
/// </summary>
public partial class SonicController
{
  public SonicController()
  {
    _effects.AddStep(CreateStep_ExitRolling());
  }

  private PipelineStep CreateStep_ExitRolling()
  {
    return PipelineStepBuilder.Create()
      .WithDisplayName("Exit Rolling")
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
