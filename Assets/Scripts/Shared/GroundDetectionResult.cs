using UnityEngine;

public class GroundDetectionResult
{
  public float Distance { get; private set; }
  public float AngleDeg { get; private set; }
  public float AngleRad { get; private set; }
  public Vector2 Contact { get; private set; }
  public Vector2 Normal { get; private set; }

  public void Update(
    float distance,
    float angleDeg,
    float angleRad,
    Vector2 contact,
    Vector2 normal)
  {
    Distance = distance;
    AngleDeg = angleDeg;
    AngleRad = angleRad;
    Contact = contact;
    Normal = normal;
  }
}
