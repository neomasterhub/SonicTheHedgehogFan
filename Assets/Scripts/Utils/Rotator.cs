public class Rotator : IRotator
{
  private readonly float _from;
  private readonly float _to;
  private readonly float _delta;

  public Rotator(float from, float to, float delta, float? curr = null)
  {
    _from = from;
    _to = to;
    _delta = delta;
    Current = curr ?? from;
  }

  public float Current { get; private set; }

  public void Reset()
  {
    Current = _from;
  }

  public float GetNext()
  {
    if (Current != _to)
    {
      Current += _delta;
    }

    return Current;
  }
}
