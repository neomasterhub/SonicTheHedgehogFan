using System.Collections.Generic;

public class Pipeline
{
  private readonly List<PipelineStep> _steps = new();

  public Pipeline AddStep(PipelineStep step)
  {
    _steps.Add(step);
    return this;
  }

  public void Run()
  {
    for (var i = 0; i < _steps.Count; i++)
    {
      var step = _steps[i];

      if (!step.Condition())
      {
        continue;
      }

      if (step.Action() == PipelineStepResult.Break)
      {
        return;
      }
    }
  }
}
