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
    return AppliedCountOp == PipelineStepAppliedCountOp.Equal
      ? $"{Result} {AppliedCount} {DisplayName}"
      : $"{Result} >{AppliedCount} {DisplayName}";
  }
}
