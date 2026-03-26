using System.Collections.Generic;

public class TimerSystem
{
  private readonly List<Timer> _timers = new();

  public void Update(float deltaTime)
  {
    for (var i = _timers.Count - 1; i > -1; i--)
    {
      var timer = _timers[i];

      timer.Update(deltaTime);

      if (timer.IsCompleted)
      {
        _timers.RemoveAt(i);
      }
    }
  }

  public void StartIfNotRunning(Timer timer)
  {
    if (_timers.Contains(timer))
    {
      return;
    }

    _timers.Add(timer);

    timer.Start();
  }
}
