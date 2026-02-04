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

    public const float AccelerationSpeed = AccelerationSpeedSpx / ConvertValues.SpxPerUnit;
    public const float DecelerationSpeed = AccelerationSpeedSpx / ConvertValues.SpxPerUnit;
    public const float FrictionSpeed = FrictionSpeedSpx / ConvertValues.SpxPerUnit;
    public const float TopSpeed = TopSpeedPx / ConvertValues.PxPerUnit;
    public const float AirAccelerationSpeed = AirAccelerationSpeedSpx / ConvertValues.SpxPerUnit;
    public const float AirTopSpeed = AirTopSpeedPx / ConvertValues.PxPerUnit;
    public const float GravityDown = GravityDownSpx / ConvertValues.SpxPerUnit;
    public const float GravityUp = GravityUpSpx / ConvertValues.SpxPerUnit;
    public const float MaxFallSpeed = MaxFallSpeedPx / ConvertValues.PxPerUnit;
  }

  public static class Sensors
  {
    public const float Length = 0.1f;

    public static readonly Dictionary<SensorId, Color> Colors = new()
    {
      [SensorId.A] = Color.yellow,
      [SensorId.B] = Color.green,
      [SensorId.C] = Color.white,
      [SensorId.D] = Color.cyan,
      [SensorId.E] = Color.magenta,
      [SensorId.F] = Color.red,
    };

    public static readonly Dictionary<SonicSizeMode, Dictionary<GroundSide, Dictionary<SensorId, SensorDef>>> Offsets = new()
    {
      [SonicSizeMode.Big] = new()
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
    }

    public static class Small
    {
      public const float HRadiusPx = 7;
      public const float VRadiusPx = 14;
      public const float HRadius = HRadiusPx / ConvertValues.PxPerUnit;
      public const float VRadius = VRadiusPx / ConvertValues.PxPerUnit;
    }
  }
}
