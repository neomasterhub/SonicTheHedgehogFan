using System;

public static class MathExtensions
{
  public static float Round(this float x, int digits = 0)
  {
    return MathF.Round(x, digits);
  }
}
