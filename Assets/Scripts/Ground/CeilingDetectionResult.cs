using UnityEngine;

public readonly struct CeilingDetectionResult
{
  public readonly char SourceSensorId;
  public readonly float Distance;
  public readonly float AngleDeg;
  public readonly float AngleRad;
  public readonly Vector2 Contact;
  public readonly Vector2 Normal;
  public readonly VerticalRelation SensorCeilingRelation;

  public CeilingDetectionResult(
    char sourceSensorId,
    RaycastHit2D hit,
    Vector2 sensorDirection,
    VerticalRelation sensorCeilingRelation)
  {
    SourceSensorId = sourceSensorId;
    Contact = hit.point;
    Normal = hit.normal;
    Distance = hit.distance;
    AngleDeg = Vector2.SignedAngle(-sensorDirection, hit.normal).Round();
    AngleRad = AngleDeg * Mathf.Deg2Rad;
    SensorCeilingRelation = sensorCeilingRelation;
  }

  public static CeilingDetectionResult CreateCDResult(
    bool horizontalDirection,
    RaycastHit2D hit,
    Vector2 sensorDirection,
    VerticalRelation sensorCeilingRelation)
  {
    return new CeilingDetectionResult(horizontalDirection ? 'D' : 'C', hit, sensorDirection, sensorCeilingRelation);
  }
}
