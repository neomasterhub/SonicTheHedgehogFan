public interface IRotator
{
  float Current { get; }
  void Reset();
  float GetNext();
}
