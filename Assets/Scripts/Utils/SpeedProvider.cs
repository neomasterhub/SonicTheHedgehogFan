using System;
using System.Collections.Generic;
using System.Linq;

public class SpeedProvider
{
  private readonly Dictionary<Func<bool>, Func<float>> _condition2speed = new();

  public Func<float> Default { get; set; } = () => 0;

  public SpeedProvider Add(Func<bool> condition, Func<float> speed)
  {
    _condition2speed.Add(condition, speed);
    return this;
  }

  public float FirstTriggeredOrDefault()
  {
    return (_condition2speed.FirstOrDefault(x => x.Key()).Value ?? Default)();
  }
}
