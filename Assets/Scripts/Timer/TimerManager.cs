using System;
using System.Collections.Generic;

public class TimerManager
{
  private readonly List<Timer> _timers = new();

  public void OnUpdate(float deltaTime)
  {
    _timers.ForEach(t => t.RemainingTime -= deltaTime);
    _timers.RemoveAll(t => t.RemainingTime <= 0);
  }

  public void RunSingle(Timer timer, Action action)
  {
    if (_timers.Contains(timer))
    {
      return;
    }

    _timers.Add(timer);
    action();
  }
}
