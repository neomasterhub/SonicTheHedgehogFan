public class Collector : ICollector
{
  public int PrevCount { get; private set; }
  public int Count { get; private set; }

  public void Add()
  {
    PrevCount = Count;
    Count++;
  }

  public void Clear()
  {
    PrevCount = Count;
    Count = 0;
  }
}
