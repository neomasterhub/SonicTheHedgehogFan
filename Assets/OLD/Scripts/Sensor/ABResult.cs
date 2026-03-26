using UnityEngine;

public struct ABResult
{
 // public SensorId? AppliedSensorId;
  public Vector2 Contact;
  public Vector2 Normal;
  public float Distance;
  public float AngleDeg;
  public float AngleRad;
  public bool GroundDetected;
  public bool BothTriggered;
  public int SensorDirectionSign;

  public void Reset()
  {
    //AppliedSensorId = null;
    Contact = Vector2.positiveInfinity;
    Normal = Vector2.zero;
    Distance = float.PositiveInfinity;
    AngleDeg = float.NaN;
    AngleRad = float.NaN;
    GroundDetected = false;
    BothTriggered = false;
    SensorDirectionSign = 1;
  }

  public void Set(
   // SensorId appliedSensorId,
    RaycastHit2D hit,
    Vector2 sensorDirection,
    int sensorDirectionSign,
    float sensorLength,
    bool bothTriggered = false)
  {
  //  AppliedSensorId = appliedSensorId;
    Contact = hit.point;
    Normal = hit.normal;
    Distance = hit.distance;
    AngleDeg = Vector2.SignedAngle(-sensorDirection, hit.normal).Round();
    AngleRad = AngleDeg * Mathf.Deg2Rad;
    GroundDetected = hit.distance <= sensorLength;
    SensorDirectionSign = sensorDirectionSign;
    BothTriggered = bothTriggered;
  }

  public readonly void DrawNormal(
    float normalLength = 1,
    float sourceRadius = 0,
    Color? color = null)
  {
    var begin = Contact;
    var end = Contact + (Normal * normalLength);

    Gizmos.color = color ?? Color.yellow;
    Gizmos.DrawLine(begin, end);
    Gizmos.DrawSphere(begin, sourceRadius);
  }
}
