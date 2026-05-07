public interface ICollector
{
  int Count { get; }
  void Add();
  void Clear();
}
