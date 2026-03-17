public class SpriteRotator
{
  private const float _initAngleDeg = 90;
  private const float _stepDeg = 5;

  private float _angleDeg;

  public void Reset()
  {
    _angleDeg = _initAngleDeg;
  }

  public float GetNextAngleDeg()
  {
    if (_angleDeg > 0)
    {
      _angleDeg -= _stepDeg;
    }

    return _angleDeg;
  }
}
