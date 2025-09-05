using UnityEngine;
using static CommonConsts;

public static class SonicConsts
{
  public static class SensorColors
  {
    public static readonly Color AColor = Color.greenYellow;
    public static readonly Color BColor = Color.green;
    public static readonly Color CColor = Color.yellow;
    public static readonly Color DColor = Color.orangeRed;
    public static readonly Color EColor = Color.magenta;
    public static readonly Color FColor = Color.red;
  }

  public static class SensorOffsets
  {
    public static class Default
    {
      public const float HRadiusPx = 9f;
      public const float VRadiusPx = 19f;
      public const float HRadius = HRadiusPx / ConvertValues.PPU;
      public const float VRadius = VRadiusPx / ConvertValues.PPU;

      public static readonly Vector2 AOffset = new(-HRadius, -VRadius);
      public static readonly Vector2 BOffset = new(HRadius, -VRadius);
      public static readonly Vector2 COffset = new(-HRadius, 0);
      public static readonly Vector2 DOffset = new(HRadius, 0);
      public static readonly Vector2 EOffset = new(-HRadius, VRadius);
      public static readonly Vector2 FOffset = new(HRadius, VRadius);
    }
  }
}
