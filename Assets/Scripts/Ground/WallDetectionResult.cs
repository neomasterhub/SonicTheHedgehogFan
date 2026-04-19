public readonly struct WallDetectionResult
{
  public readonly float Distance;
  public readonly float AngleDeg;

  public WallDetectionResult(float distance, float angleDeg)
  {
    Distance = distance;
    AngleDeg = angleDeg;
  }
}
