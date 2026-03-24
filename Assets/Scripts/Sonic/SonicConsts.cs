using System.Collections.Generic;
using UnityEngine;
using static Consts;

public static class SonicConsts
{
  public static class Physics
  {
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

  public static class Sensors
  {
    public static readonly Dictionary<SensorId, Color> EnabledColors = new()
    {
      [SensorId.A] = Color.orange,
      [SensorId.B] = Color.red,
      [SensorId.C] = Color.khaki,
      [SensorId.D] = Color.green,
      [SensorId.E] = Color.cyan,
      [SensorId.F] = Color.blue,
    };

    public static readonly Dictionary<SensorId, Color> DisabledColors = new()
    {
      [SensorId.A] = Color.darkOrange,
      [SensorId.B] = Color.darkRed,
      [SensorId.C] = Color.darkKhaki,
      [SensorId.D] = Color.darkGreen,
      [SensorId.E] = Color.darkCyan,
      [SensorId.F] = Color.darkBlue,
    };

    public static readonly Dictionary<SizeMode, Dictionary<GroundSide, Dictionary<SensorId, SensorDef>>> Offsets = new()
    {
      [SizeMode.Big] = new()
      {
        [GroundSide.Down] = new()
        {
          [SensorId.A] = new(new(-Sizes.Big.HRadius, -Sizes.Big.VRadius), Vector2.down),
          [SensorId.B] = new(new(Sizes.Big.HRadius, -Sizes.Big.VRadius), Vector2.down),
          [SensorId.C] = new(new(-Sizes.Big.HRadius, 0), Vector2.left),
          [SensorId.D] = new(new(Sizes.Big.HRadius, 0), Vector2.right),
          [SensorId.E] = new(new(-Sizes.Big.HRadius, Sizes.Big.VRadius), Vector2.up),
          [SensorId.F] = new(new(Sizes.Big.HRadius, Sizes.Big.VRadius), Vector2.up),
        },
        [GroundSide.Right] = new()
        {
          [SensorId.A] = new(new(Sizes.Big.VRadius, -Sizes.Big.HRadius), Vector2.right),
          [SensorId.B] = new(new(Sizes.Big.VRadius, Sizes.Big.HRadius), Vector2.right),
          [SensorId.C] = new(new(0, -Sizes.Big.HRadius), Vector2.down),
          [SensorId.D] = new(new(0, Sizes.Big.HRadius), Vector2.up),
          [SensorId.E] = new(new(-Sizes.Big.VRadius, -Sizes.Big.HRadius), Vector2.left),
          [SensorId.F] = new(new(-Sizes.Big.VRadius, Sizes.Big.HRadius), Vector2.left),
        },
        [GroundSide.Up] = new()
        {
          [SensorId.F] = new(new(-Sizes.Big.HRadius, -Sizes.Big.VRadius), Vector2.down),
          [SensorId.E] = new(new(Sizes.Big.HRadius, -Sizes.Big.VRadius), Vector2.down),
          [SensorId.D] = new(new(-Sizes.Big.HRadius, 0), Vector2.left),
          [SensorId.C] = new(new(Sizes.Big.HRadius, 0), Vector2.right),
          [SensorId.B] = new(new(-Sizes.Big.HRadius, Sizes.Big.VRadius), Vector2.up),
          [SensorId.A] = new(new(Sizes.Big.HRadius, Sizes.Big.VRadius), Vector2.up),
        },
        [GroundSide.Left] = new()
        {
          [SensorId.F] = new(new(Sizes.Big.VRadius, -Sizes.Big.HRadius), Vector2.right),
          [SensorId.E] = new(new(Sizes.Big.VRadius, Sizes.Big.HRadius), Vector2.right),
          [SensorId.D] = new(new(0, -Sizes.Big.HRadius), Vector2.down),
          [SensorId.C] = new(new(0, Sizes.Big.HRadius), Vector2.up),
          [SensorId.B] = new(new(-Sizes.Big.VRadius, -Sizes.Big.HRadius), Vector2.left),
          [SensorId.A] = new(new(-Sizes.Big.VRadius, Sizes.Big.HRadius), Vector2.left),
        },
      },
    };
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

  public static class Times
  {
    public const float PostDetachInputUnlockTimerSeconds = 0.5f;
  }
}
