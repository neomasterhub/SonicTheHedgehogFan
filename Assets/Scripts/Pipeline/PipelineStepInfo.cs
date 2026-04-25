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
}
