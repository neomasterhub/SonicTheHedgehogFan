public interface ISpriteRotator
{
  float Current { get; }
  void Reset();
  float GetNext();
}
