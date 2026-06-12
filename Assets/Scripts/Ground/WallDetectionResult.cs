using UnityEngine;

public readonly struct WallDetectionResult
{
  public readonly float Distance;
  public readonly float AngleDeg;
  public readonly Transform ContactTransform;

  public WallDetectionResult(
    RaycastHit2D hit,
    Vector2 sensorDirection)
  {
    Distance = hit.distance;
    AngleDeg = Vector2.SignedAngle(-sensorDirection, hit.normal).Round();
    ContactTransform = hit.transform;
  }
}
