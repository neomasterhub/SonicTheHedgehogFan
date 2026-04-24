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

  public string[] GetHistory()
  {
    var history = new string[_history.Capacity];
    var j = 0;

    for (var i = 0; i < _history.Right.Length; i++, j++)
    {
      history[j] = _history.Right[i];
    }

    for (var i = 0; i < _history.Left.Length; i++, j++)
    {
      history[j] = _history.Left[i];
    }

    return history;
  }
}
