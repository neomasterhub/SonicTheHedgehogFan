using System;

public class PipelineStepBuilder
{
  private string _displayName;
  private Func<bool> _condition;
  private Func<PipelineStepResult> _action;

  private PipelineStepBuilder()
  {
  }

  public static PipelineStepBuilder Create()
  {
    return new();
  }

  public PipelineStepBuilder WithDisplayName(string displayName)
  {
    _displayName = displayName;
    return this;
  }

  public PipelineStepBuilder WithCondition(Func<bool> condition)
  {
    _condition = condition;
    return this;
  }

  public PipelineStepBuilder WithAction(Func<PipelineStepResult> action)
  {
    _action = action;
    return this;
  }

  public PipelineStep Build()
  {
    return new(_displayName, _condition, _action);
  }
}
