using System;
using UnityEngine;
using static SharedConsts.Physics;

public static class MathExtensions
{
  public static float Round(this float x, int digits = 0)
  {
    return MathF.Round(x, digits);
  }

  public static Vector3 RoundPosition2D(this Vector3 v)
  {
    return new Vector3(v.x.Round(PositionRoundingDigits), v.y.Round(PositionRoundingDigits));
  }
}
