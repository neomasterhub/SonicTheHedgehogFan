using UnityEngine;

public readonly struct GroundDetectionResult
{
  public readonly char SourceSensorId;
  public readonly float Distance;
  public readonly float AngleDeg;
  public readonly float AngleRad;
  public readonly Vector2 Contact;
  public readonly Vector2 Normal;
  public readonly VerticalRelation SensorGroundRelation;
  public readonly Transform ContactTransform;
  public readonly bool IsBalancing;

  public GroundDetectionResult(
    char sourceSensorId,
    RaycastHit2D hit,
    Vector2 sensorDirection,
    VerticalRelation sensorGroundRelation,
    bool isBalancing = false)
  {
    SourceSensorId = sourceSensorId;
    Contact = hit.point;
    Normal = hit.normal;
    Distance = hit.distance;
    AngleDeg = Vector2.SignedAngle(-sensorDirection, hit.normal).Round();
    AngleRad = AngleDeg * Mathf.Deg2Rad;
    SensorGroundRelation = sensorGroundRelation;
    ContactTransform = hit.transform;
    IsBalancing = isBalancing;
  }

  public static GroundDetectionResult CreateABResult(
    bool horizontalDirection,
    RaycastHit2D hit,
    Vector2 sensorDirection,
    VerticalRelation sensorGroundRelation,
    bool isBalancing = false)
  {
    return new GroundDetectionResult(horizontalDirection ? 'B' : 'A', hit, sensorDirection, sensorGroundRelation, isBalancing);
  }
}
