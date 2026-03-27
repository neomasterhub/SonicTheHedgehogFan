using UnityEngine;

public class GroundDetectionResult
{
  public bool Detected { get; private set; }
  public float? Distance { get; private set; }
  public float? AngleDeg { get; private set; }
  public float? AngleRad { get; private set; }
  public Vector2? Contact { get; private set; }
  public Vector2? Normal { get; private set; }

  public void Reset()
  {
    Detected = false;
    Distance = null;
    AngleDeg = null;
    AngleRad = null;
    Contact = null;
    Normal = null;
  }

  public void Update(RaycastHit2D hit, Vector2 sensorDirection)
  {
    Detected = true;
    Contact = hit.point;
    Normal = hit.normal;
    Distance = hit.distance;
    AngleDeg = Vector2.SignedAngle(-sensorDirection, hit.normal).Round();
    AngleRad = AngleDeg * Mathf.Deg2Rad;
  }

  public void DrawNormal()
  {
    if (Detected)
    {
      Gizmos.color = Color.white;
      Gizmos.DrawLine(Contact.Value, Contact.Value + Normal.Value);
    }
  }
}
