using UnityEngine;
using static SharedConsts.ConvertValues;

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
    public const float GravityDownSpeedSpx = 56;
    public const float GravityUpSpeedSpx = 16;
    public const float MaxFallSpeedPx = 16;
    public const float SlopeFactorSpx = 32;

    public const float AccelerationSpeed = AccelerationSpeedSpx / SpxPerUnit;
    public const float DecelerationSpeed = DecelerationSpeedSpx / SpxPerUnit;
    public const float FrictionSpeed = FrictionSpeedSpx / SpxPerUnit;
    public const float TopSpeed = TopSpeedPx / PxPerUnit;
    public const float AirAccelerationSpeed = AirAccelerationSpeedSpx / SpxPerUnit;
    public const float AirTopSpeed = AirTopSpeedPx / PxPerUnit;
    public const float GravityDownSpeed = GravityDownSpeedSpx / SpxPerUnit;
    public const float GravityUpSpeed = GravityUpSpeedSpx / SpxPerUnit;
    public const float MaxFallSpeed = MaxFallSpeedPx / PxPerUnit;
    public const float SlopeFactor = SlopeFactorSpx / SpxPerUnit;
  }

  public static class Sizes
  {
    public static class Big
    {
      public const float HRadiusPx = 9;
      public const float VRadiusPx = 19;
      public const float HRadius = HRadiusPx / PxPerUnit;
      public const float VRadius = VRadiusPx / PxPerUnit;
      public static Vector2 HVRadii = new(HRadius, VRadius);
    }

    public static class Small
    {
      public const float HRadiusPx = 7;
      public const float VRadiusPx = 14;
      public const float HRadius = HRadiusPx / PxPerUnit;
      public const float VRadius = VRadiusPx / PxPerUnit;
      public static Vector2 HVRadii = new(HRadius, VRadius);
    }
  }

  public static class View
  {
    public const float SpeedAirborneMin = 0.02f;
    public const float SpeedWalkingMin = 0.5f;
    public const float SpeedWalkingFactor = 3;
    public const float AngleDegStandingStraightMax = 38;
  }
}
