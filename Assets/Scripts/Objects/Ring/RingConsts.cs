using static SharedConsts.UI;

public static class RingConsts
{
  public static class UI
  {
    public const int SparkleOrderInLayer = PlayerOrderInLayer + 1;
  }

  public static class Physics
  {
    public const float SensorY = 0.075f;
    public const float InnerSensorRayLength = 0.2f;
    public const float OuterSensorRayLength = 0.1f;
    public static readonly RingPhysicsModeConfig NormalConfig = new(0.003f, 0.8f);
  }
}
