using UnityEngine;

public class GroundDetectionResult
{
  public float Distance { get; private set; }
  public float AngleDeg { get; private set; }
  public float AngleRad { get; private set; }
  public Vector2 Contact { get; private set; }
  public Vector2 Normal { get; private set; }

  public void Update(RaycastHit2D hit, Vector2 sensorDirection)
  {
    Contact = hit.point;
    Normal = hit.normal;
    Distance = hit.distance;
    AngleDeg = Vector2.SignedAngle(-sensorDirection, hit.normal).Round();
    AngleRad = AngleDeg * Mathf.Deg2Rad;
  }
}
