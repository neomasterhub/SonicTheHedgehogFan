public class SpriteRotator : ISpriteRotator
{
  private readonly float _from;
  private readonly float _to;
  private readonly float _delta;

  private float _curr;

  public SpriteRotator(float from, float to, float delta, float? curr = null)
  {
    _from = from;
    _to = to;
    _delta = delta;
    _curr = curr ?? from;
  }

  public void Reset()
  {
    _curr = _from;
  }

  public float GetNext()
  {
    if (_curr != _to)
    {
      _curr += _delta;
    }

    return _curr;
  }
}
