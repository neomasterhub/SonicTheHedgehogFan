using System;

public readonly struct PipelineStep
{
  public readonly string DisplayName;
  public readonly Func<bool> Condition;
  public readonly Func<PipelineStepResult> Action;

  public PipelineStep(string displayName, Func<bool> condition, Func<PipelineStepResult> action)
  {
    DisplayName = displayName;
    Condition = condition;
    Action = action;
  }
}
