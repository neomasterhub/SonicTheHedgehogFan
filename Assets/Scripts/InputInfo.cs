using System;

public class InputInfo
{
  private readonly Func<float> _xGetter;
  private readonly Func<float> _yGetter;

  public InputInfo(Func<float> xGetter, Func<float> yGetter)
  {
    _xGetter = xGetter;
    _yGetter = yGetter;
  }

  public float X { get; private set; }
  public float Y { get; private set; }
  public float XDirection { get; private set; }
  public float YDirection { get; private set; }

  public void Update()
  {
    X = _xGetter();
    Y = _yGetter();
    XDirection = Math.Sign(X);
    YDirection = Math.Sign(Y);
  }
}
