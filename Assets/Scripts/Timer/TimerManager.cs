using System.Collections.Generic;

public class TimerManager
{
  private readonly List<Timer> _timers = new();

  public void OnUpdate(float deltaTime)
  {
    for (var i = _timers.Count - 1; i >= 0; i--)
    {
      var timer = _timers[i];

      timer.Update(deltaTime);

      if (timer.IsCompleted)
      {
        _timers.RemoveAt(i);
      }
    }
  }

  public void RunSingle(Timer timer)
  {
    if (_timers.Contains(timer))
    {
      return;
    }

    _timers.Add(timer);
    timer.Start();
  }
}
