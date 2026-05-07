public class Collector : ICollector
{
  public int PrevCount { get; private set; }
  public int Count { get; private set; }

  public void Add(int value = 1)
  {
    PrevCount = Count;
    Count += value;
  }

  public void Clear()
  {
    PrevCount = Count;
    Count = 0;
  }
}
