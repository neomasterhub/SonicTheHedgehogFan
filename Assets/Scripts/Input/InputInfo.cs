using System;

public class InputInfo
{
  private readonly Func<float> _xSrc;
  private readonly Func<float> _ySrc;

  public InputInfo(
    Func<float> xSrc,
    Func<float> ySrc)
  {
    _xSrc = xSrc;
    _ySrc = ySrc;
  }

  public bool Enabled { get; set; } = true;
  public float X { get; private set; }
  public float Y { get; private set; }
  public float XDirection { get; private set; }
  public float YDirection { get; private set; }

  public void Update()
  {
    if (Enabled)
    {
      X = _xSrc();
      Y = _ySrc();
      XDirection = Math.Sign(X);
      YDirection = Math.Sign(Y);
    }
    else
    {
      X = 0;
      Y = 0;
    }
  }
}
