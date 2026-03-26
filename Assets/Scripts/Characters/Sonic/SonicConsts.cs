using UnityEngine;
using static SharedConsts;

public static class SonicConsts
{
  public static class Physics
  {
    public const float InputUnlockTimerSeconds = 0.5f;

    public const float AccelerationSpeedSpx = 12;
    public const float DecelerationSpeedSpx = 128;
    public const float FrictionSpeedSpx = 12;
    public const float TopSpeedPx = 6;
    public const float AirAccelerationSpeedSpx = 14;
    public const float AirTopSpeedPx = 6;
    public const float GravityDownSpx = 56;
    public const float GravityUpSpx = 16;
    public const float MaxFallSpeedPx = 16;
    public const float SlopeFactorSpx = 32;

    public const float AccelerationSpeed = AccelerationSpeedSpx / ConvertValues.SpxPerUnit;
    public const float DecelerationSpeed = DecelerationSpeedSpx / ConvertValues.SpxPerUnit;
    public const float FrictionSpeed = FrictionSpeedSpx / ConvertValues.SpxPerUnit;
    public const float TopSpeed = TopSpeedPx / ConvertValues.PxPerUnit;
    public const float AirAccelerationSpeed = AirAccelerationSpeedSpx / ConvertValues.SpxPerUnit;
    public const float AirTopSpeed = AirTopSpeedPx / ConvertValues.PxPerUnit;
    public const float GravityDown = GravityDownSpx / ConvertValues.SpxPerUnit;
    public const float GravityUp = GravityUpSpx / ConvertValues.SpxPerUnit;
    public const float MaxFallSpeed = MaxFallSpeedPx / ConvertValues.PxPerUnit;
    public const float SlopeFactor = SlopeFactorSpx / ConvertValues.SpxPerUnit;
  }

  public static class Sizes
  {
    public static class Big
    {
      public const float HRadiusPx = 9;
      public const float VRadiusPx = 19;
      public const float HRadius = HRadiusPx / ConvertValues.PxPerUnit;
      public const float VRadius = VRadiusPx / ConvertValues.PxPerUnit;
      public static Vector2 HVRadii = new(HRadius, VRadius);
    }

    public static class Small
    {
      public const float HRadiusPx = 7;
      public const float VRadiusPx = 14;
      public const float HRadius = HRadiusPx / ConvertValues.PxPerUnit;
      public const float VRadius = VRadiusPx / ConvertValues.PxPerUnit;
      public static Vector2 HVRadii = new(HRadius, VRadius);
    }
  }
}
