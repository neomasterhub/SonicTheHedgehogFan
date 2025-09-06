using UnityEngine;
using static CommonConsts;

public static class SonicConsts
{
  public static class Sensors
  {
    public static class Colors
    {
      public static readonly Color A = Color.yellow;
      public static readonly Color B = Color.green;
      public static readonly Color C = Color.cyan;
      public static readonly Color D = Color.blue;
      public static readonly Color E = Color.magenta;
      public static readonly Color F = Color.red;
    }

    public static class Offsets
    {
      public static class Big
      {
        public static class OnGround
        {
          public static readonly Vector2 A = new(-Sizes.Big.HRadius, -Sizes.Big.VRadius);
          public static readonly Vector2 B = new(Sizes.Big.HRadius, -Sizes.Big.VRadius);
          public static readonly Vector2 C = new(-Sizes.Big.HRadius, 0);
          public static readonly Vector2 D = new(Sizes.Big.HRadius, 0);
          public static readonly Vector2 E = new(-Sizes.Big.HRadius, Sizes.Big.VRadius);
          public static readonly Vector2 F = new(Sizes.Big.HRadius, Sizes.Big.VRadius);
        }
      }
    }
  }

  public static class Sizes
  {
    public static class Big
    {
      public const float HRadiusPx = 9f;
      public const float VRadiusPx = 19f;
      public const float HRadius = HRadiusPx / ConvertValues.PPU;
      public const float VRadius = VRadiusPx / ConvertValues.PPU;
    }

    public static class Small
    {
      public const float HRadiusPx = 7f;
      public const float VRadiusPx = 14f;
      public const float HRadius = HRadiusPx / ConvertValues.PPU;
      public const float VRadius = VRadiusPx / ConvertValues.PPU;
    }
  }
}
