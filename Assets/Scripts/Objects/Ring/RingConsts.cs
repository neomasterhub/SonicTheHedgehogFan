using UnityEngine;
using static SharedConsts.ConvertValues;
using static SharedConsts.UI;

public static class RingConsts
{
  public static class UI
  {
    public const int SparkleOrderInLayer = PlayerOrderInLayer + 1;
  }

  public static class Physics
  {
    private const float _lostPortion1SpeedPx = 4;
    private const float _lostPortion2SpeedPx = 2;
    private const float _lostAngleStepDeg = 22.5f;
    private const float _lostInitialAngleDeg = 101.25f;

    public const int MaxLostNumber = 32;
    public const float MaxStopSpeed = 0.002f;
    public const float SensorY = 0.075f;
    public const float InnerSensorRayLength = 0.2f;
    public const float OuterSensorRayLength = 0.1f;
    public const float LostLifetime = 4.27f;
    public const float LostPortion1Speed = _lostPortion1SpeedPx / PxPerUnit;
    public const float LostPortion2Speed = _lostPortion2SpeedPx / PxPerUnit;
    public const float LostAngleStepRad = _lostAngleStepDeg * Mathf.Deg2Rad;
    public const float LostInitialAngleRad = _lostInitialAngleDeg * Mathf.Deg2Rad;
    public static readonly RingPhysicsModeConfig NormalConfig = new(0.003f, 0.8f);
  }
}
