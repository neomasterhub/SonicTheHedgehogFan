public interface ICollector
{
  int Count { get; }
  void Add(int value = 1);
  void Clear();
}
