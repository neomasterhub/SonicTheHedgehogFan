using UnityEngine;

public readonly struct GroundDetectionResult
{
  public readonly float Distance;
  public readonly float AngleDeg;
  public readonly float AngleRad;
  public readonly Vector2 Contact;
  public readonly Vector2 Normal;
  public readonly VerticalSide SensorGroundSide;
  public readonly bool IsBalancing;

  public GroundDetectionResult(
    RaycastHit2D hit,
    Vector2 sensorDirection,
    VerticalSide sensorGroundSide = VerticalSide.Above,
    bool isBalancing = false)
  {
    Contact = hit.point;
    Normal = hit.normal;
    Distance = hit.distance;
    AngleDeg = Vector2.SignedAngle(-sensorDirection, hit.normal).Round();
    AngleRad = AngleDeg * Mathf.Deg2Rad;
    SensorGroundSide = sensorGroundSide;
    IsBalancing = isBalancing;
  }
}
