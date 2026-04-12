using UnityEngine;

public readonly struct GroundDetectionResult
{
  public readonly float Distance;
  public readonly float AngleDeg;
  public readonly float AngleRad;
  public readonly Vector2 Contact;
  public readonly Vector2 Normal;
  public readonly VerticalRelation SensorGroundRelation;
  public readonly HorizontalDirection BalancingSide;

  public GroundDetectionResult(
    RaycastHit2D hit,
    Vector2 sensorDirection,
    VerticalRelation sensorGroundRelation = VerticalRelation.Above,
    HorizontalDirection balancingSide = HorizontalDirection.None)
  {
    Contact = hit.point;
    Normal = hit.normal;
    Distance = hit.distance;
    AngleDeg = Vector2.SignedAngle(-sensorDirection, hit.normal).Round();
    AngleRad = AngleDeg * Mathf.Deg2Rad;
    SensorGroundRelation = sensorGroundRelation;
    BalancingSide = balancingSide;
  }
}
