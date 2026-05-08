using System;

public class Collector : ICollector
{
  private event Action Added;
  private event Action Cleared;

  public int Count { get; private set; }

  public void Clear()
  {
    Count = 0;
    Cleared?.Invoke();
  }

  public void Add(int value = 1)
  {
    Count += value;
    Added?.Invoke();
  }

  public ICollector WhenAdded(Action action)
  {
    Added += action;
    return this;
  }

  public ICollector WhenCleared(Action action)
  {
    Cleared += action;
    return this;
  }
}
