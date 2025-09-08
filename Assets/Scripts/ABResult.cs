using UnityEngine;

public struct ABResult
{
  public Vector2 Contact;
  public Vector2 Normal;
  public float Distance;
  public float AngleDeg;
  public float AngleRad;
  public bool GroundDetected;

  public void Reset()
  {
    Contact = Vector2.positiveInfinity;
    Normal = Vector2.zero;
    Distance = float.PositiveInfinity;
    AngleDeg = float.NaN;
    AngleRad = float.NaN;
    GroundDetected = false;
  }

  public void Set(
    RaycastHit2D hit,
    Vector2 sensorDirection,
    float sensorLength)
  {
    Contact = hit.point;
    Normal = hit.normal;
    Distance = hit.distance;
    AngleDeg = Vector2.SignedAngle(-sensorDirection, hit.normal);
    AngleRad = AngleDeg * Mathf.Deg2Rad;
    GroundDetected = hit.distance <= sensorLength;
  }

  public readonly void Draw(
    float normalLength = 1,
    float beginRadius = 0,
    float endRadius = 0,
    Color? color = null)
  {
    var begin = Contact;
    var end = Contact + (Normal * normalLength);

    Gizmos.color = color ?? Color.yellow;
    Gizmos.DrawLine(begin, end);
    Gizmos.DrawSphere(begin, beginRadius);
    Gizmos.DrawSphere(end, endRadius);
  }
}
