using UnityEngine;

public class GroundDetectionResult
{
  public float Distance { get; private set; }
  public float AngleDeg { get; private set; }
  public float AngleRad { get; private set; }
  public Vector2 Contact { get; private set; }
  public Vector2 Normal { get; private set; }
}
