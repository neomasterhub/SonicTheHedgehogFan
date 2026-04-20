using UnityEngine;
using static SharedConsts.ConvertValues;

public static class SonicConsts
{
  public static class Physics
  {
    public const float InputUnlockTimerSeconds = 0.5f;
    public const float MaxSkiddingSpeed = 0.1f;

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

    public const float OLength = Sizes.Big.VRadius + 0.4f;
    public static readonly Vector3 TopUDFLengths = new(0.3f, 0.3f, 0.5f);
    public static readonly Vector3 BottomUDFLengths = new(0.3f, 0.1f, 0.5f);
    public static readonly Vector2 WallToAirSpeedDelta = new(0.011f, 0);
    public static readonly Vector2 WallDetachPositionOffset = new(-0.1f, 0);
  }

  public static class Sizes
  {
    public const float VRadiusDelta = Big.VRadius - Small.VRadius;

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
      public const float HRadiusPx = 9;
      public const float VRadiusPx = 14;
      public const float HRadius = HRadiusPx / PxPerUnit;
      public const float VRadius = VRadiusPx / PxPerUnit;
      public static Vector2 HVRadii = new(HRadius, VRadius);
    }
  }

  public static class View
  {
    public const float AirborneSpeedMin = 0.02f;
    public const float WalkingSpeedMin = 0.5f;
    public const float WalkingSpeedFactor = 3;
    public const float StandingStraightAngleDegMax = 38;
    public const float WallToAirViewRotatorAngleDegDelta = 3;
  }
}
