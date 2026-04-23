/// <summary>
/// Effects.
/// </summary>
public partial class SonicController
{
  public SonicController()
  {
    var eExitRolling = PipelineStepBuilder.Create()
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

    _effects.AddStep(eExitRolling);
  }
}
