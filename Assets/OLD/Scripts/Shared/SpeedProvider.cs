using System;
using System.Collections.Generic;
using System.Linq;

public class SpeedProvider<TSpeed>
  where TSpeed : struct
{
  private readonly Dictionary<Func<bool>, Func<TSpeed>> _condition2speed = new();

  public Func<TSpeed> Default { get; set; } = () => default;

  public SpeedProvider<TSpeed> Add(Func<bool> condition, Func<TSpeed> speed)
  {
    _condition2speed.Add(condition, speed);
    return this;
  }

  public TSpeed FirstTriggeredOrDefault()
  {
    return (_condition2speed.FirstOrDefault(x => x.Key()).Value ?? Default)();
  }
}
