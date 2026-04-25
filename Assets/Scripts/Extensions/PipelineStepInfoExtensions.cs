public static class PipelineStepInfoExtensions
{
  public static string ToEffectString(this PipelineStepInfo stepInfo)
  {
    if (stepInfo.AppliedCount == 0)
    {
      return string.Empty;
    }

    return stepInfo.AppliedCountOp == PipelineStepAppliedCountOp.Equal
      ? $"{stepInfo.DisplayName,-24} {stepInfo.Result.ToShortString()} {stepInfo.AppliedCount}"
      : $"{stepInfo.DisplayName,-24} {stepInfo.Result.ToShortString()} {stepInfo.AppliedCount}+";
  }
}
