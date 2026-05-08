public static class RingConsts
{
  public static class Physics
  {
    public const float InnerSensorRayLength = 0.3f;
    public const float OuterSensorRayLength = 0.15f;
    public static readonly RingPhysicsModeConfig NormalConfig = new(0.005f, 0.8f, 0.02f);
  }
}
