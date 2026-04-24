using System.Collections.Generic;
using Neomaster.RingBuffer;

public class Pipeline
{
  private readonly List<PipelineStep> _steps;
  private readonly RingBuffer<string> _history;

  public Pipeline(int historyCapacity = 3)
  {
    _steps = new();
    _history = new(historyCapacity);
  }

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

      _history.Push(step.DisplayName);

      if (step.Action() == PipelineStepResult.Break)
      {
        return;
      }
    }
  }
}
