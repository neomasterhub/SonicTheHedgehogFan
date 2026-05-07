public class Collector : ICollector
{
  public int Count { get; private set; }

  public void Add(int value = 1)
  {
    Count += value;
  }

  public void Clear()
  {
    Count = 0;
  }
}
