using UnityEngine;
using static SharedConsts.ConvertValues;

public static class SonicConsts
{
  public static class Physics
  {
    private const float AccelerationSpeedSpx = 12;
    private const float DecelerationSpeedSpx = 128;
    private const float FrictionSpeedSpx = 12;
    private const float TopSpeedPx = 6;
    private const float AirAccelerationSpeedSpx = 14;
    private const float AirTopSpeedPx = 6;
    private const float GravityDownSpeedSpx = 56;
    private const float GravityUpSpeedSpx = 16;
    private const float MaxFallSpeedPx = 16;
    private const float SlopeFactorSpx = 32;
    private const float RollDecelerationSpeedSpx = 32;
    private const float RollFrictionSpeedSpx = 6;
    private const float RollUphillSlopeFactorSpx = 20;
    private const float RollDownhillSlopeFactorSpx = 80;
    private const float JumpSpeedPx = 3.8f;
    private const float JumpCutoffSpeedPx = 2;

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
    public const float RollDecelerationSpeed = RollDecelerationSpeedSpx / SpxPerUnit;
    public const float RollFrictionSpeed = RollFrictionSpeedSpx / SpxPerUnit;
    public const float RollUphillSlopeFactor = RollUphillSlopeFactorSpx / SpxPerUnit;
    public const float RollDownhillSlopeFactor = RollDownhillSlopeFactorSpx / SpxPerUnit;
    public const float JumpSpeed = JumpSpeedPx / PxPerUnit;
    public const float JumpCutoffSpeed = JumpCutoffSpeedPx / PxPerUnit;

    public const float MinSkiddingSpeed = 0.1f;
    public const float InputUnlockTimerSeconds = 0.5f;
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
      public const float HRadius = 0.23f;
      public const float VRadius = 0.44f;
      public static Vector2 HVRadii = new(HRadius, VRadius);
    }

    public static class Small
    {
      public const float HRadius = 0.23f;
      public const float VRadius = 0.28f;
      public static Vector2 HVRadii = new(HRadius, VRadius);
    }
  }

  public static class View
  {
    public const float AirborneSpeedMin = 0.02f;
    public const float RollingSpeedMin = 1.5f;
    public const float RollingSpeedFactor = 3;
    public const float WalkingSpeedMin = 0.5f;
    public const float WalkingSpeedFactor = 3;
    public const float StandingStraightAngleDegMax = 38;
    public const float WallToAirViewRotatorAngleDegDelta = 3;
  }
}
