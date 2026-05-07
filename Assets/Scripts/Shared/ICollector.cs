using System;

public interface ICollector
{
  int Count { get; }
  void Clear();
  void Add(int value = 1);
  ICollector WhenAdded(Action action);
  ICollector WhenCleared(Action action);
}
