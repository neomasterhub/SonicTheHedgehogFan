using System.Collections.Generic;
using Neomaster.RingBuffer;

public class Pipeline
{
  private readonly int _lastAppliedCountMax;
  private readonly List<PipelineStep> _steps;
  private readonly PipelineStepInfo[] _appliedHistory;
  private readonly RingBuffer<PipelineStepInfo> _prevAppliedHistory;

  private PipelineStepInfo _lastApplied;

  public Pipeline(int prevAppliedHistoryCapacity = 2, int lastAppliedCountMax = 3)
  {
    _steps = new();
    _appliedHistory = new PipelineStepInfo[prevAppliedHistoryCapacity + 1];
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
        if (step.DisplayName == _lastApplied.DisplayName
          && result == _lastApplied.Result)
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
        else
        {
          _prevAppliedHistory.Push(_lastApplied);
          _lastApplied = new(step.DisplayName, result);
        }
      }

      if (result == PipelineStepResult.Break)
      {
        return;
      }
    }
  }

  public PipelineStepInfo[] GetAppliedHistory()
  {
    _appliedHistory[0] = _lastApplied;
    var j = 1;

    for (var i = _prevAppliedHistory.Left.Length - 1; i > -1; i--, j++)
    {
      _appliedHistory[j] = _prevAppliedHistory.Left[i];
    }

    for (var i = _prevAppliedHistory.Right.Length - 1; i > -1; i--, j++)
    {
      _appliedHistory[j] = _prevAppliedHistory.Right[i];
    }

    return _appliedHistory;
  }
}
