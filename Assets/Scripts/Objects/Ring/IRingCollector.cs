public interface IRingCollector
{
  bool CanCollectRing { get; }
  ICollector Rings { get; }
}
