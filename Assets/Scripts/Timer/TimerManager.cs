using System;
using System.Collections.Generic;
using System.Linq;

public class TimerManager
{
  private readonly Dictionary<string, float> _timers = new();

  public void OnUpdate(float deltaTime)
  {
    foreach (var key in _timers.Keys.ToArray())
    {
      _timers[key] -= deltaTime;
      if (_timers[key] <= 0)
      {
        _timers.Remove(key);
      }
    }
  }

  public void RunSingle(string key, float remainingTime, Action action)
  {
    if (_timers.ContainsKey(key))
    {
      return;
    }

    _timers.Add(key, remainingTime);
    action();
  }
}
