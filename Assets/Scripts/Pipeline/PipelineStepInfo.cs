public struct PipelineStepInfo
{
  public readonly string DisplayName;
  public int AppliedCount;
  public PipelineStepResult Result;
  public PipelineStepAppliedCountOp AppliedCountOp;

  public PipelineStepInfo(string displayName, PipelineStepResult result)
  {
    AppliedCount = 1;
    DisplayName = displayName;
    Result = result;
    AppliedCountOp = PipelineStepAppliedCountOp.Equal;
  }

  public override readonly string ToString()
  {
    if (AppliedCount == 0)
    {
      return string.Empty;
    }

    return AppliedCountOp == PipelineStepAppliedCountOp.Equal
      ? $" {AppliedCount,2} {Result.ToShortString()} {DisplayName}"
      : $">{AppliedCount,2} {Result.ToShortString()} {DisplayName}";
  }
}
