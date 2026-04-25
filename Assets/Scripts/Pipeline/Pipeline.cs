using System.Collections.Generic;
using Neomaster.RingBuffer;

public class Pipeline
{
  private readonly int _lastAppliedCountMax;
  private readonly List<PipelineStep> _steps;
  private readonly RingBuffer<PipelineStepInfo> _prevAppliedHistory;

  private PipelineStepInfo _lastApplied;

  public Pipeline(int prevAppliedHistoryCapacity = 5, int lastAppliedCountMax = 10)
  {
    _steps = new();
    _prevAppliedHistory = new(prevAppliedHistoryCapacity);
    _lastAppliedCountMax = lastAppliedCountMax;
  }

  public Pipeline AddStep(PipelineStep step)
  {
    _steps.Add(step);
    return this;
  }

  public void Run(bool writeAppliedHistory = true)
  {
    for (var i = 0; i < _steps.Count; i++)
    {
      var step = _steps[i];

      if (!step.Condition())
      {
        continue;
      }

      var result = step.Action();

      if (writeAppliedHistory)
      {
        if (step.DisplayName != _lastApplied.DisplayName
          && result != _lastApplied.Result)
        {
          if (_lastApplied.AppliedCount > 0)
          {
            _prevAppliedHistory.Push(_lastApplied);
          }

          _lastApplied = new(step.DisplayName, result);
        }
        else
        {
          if (_lastApplied.AppliedCount + 1 > _lastAppliedCountMax)
          {
            _lastApplied.AppliedCountOp = PipelineStepAppliedCountOp.GreaterThan;
          }
          else
          {
            _lastApplied.AppliedCount++;
          }
        }
      }

      if (result == PipelineStepResult.Break)
      {
        return;
      }
    }
  }
}
