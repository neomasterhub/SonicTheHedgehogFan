using System.Collections.Generic;
using System.Text;
using Neomaster.RingBuffer;

public class Pipeline
{
  private readonly int _lastAppliedCountMax;
  private readonly List<PipelineStep> _steps;
  private readonly StringBuilder _appliedHistory;
  private readonly RingBuffer<PipelineStepInfo> _prevAppliedHistory;

  private PipelineStepInfo _lastApplied;

  public Pipeline(int prevAppliedHistoryCapacity = 2, int lastAppliedCountMax = 3)
  {
    _steps = new();
    _appliedHistory = new();
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

  public string GetAppliedHistoryString()
  {
    _appliedHistory.Clear();

    _appliedHistory.AppendLine(_lastApplied.ToString());

    for (var i = _prevAppliedHistory.Left.Length - 1; i > -1; i--)
    {
      _appliedHistory.AppendLine(_prevAppliedHistory.Left[i].ToString());
    }

    for (var i = _prevAppliedHistory.Right.Length - 1; i > -1; i--)
    {
      _appliedHistory.AppendLine(_prevAppliedHistory.Right[i].ToString());
    }

    return _appliedHistory.ToString();
  }
}
