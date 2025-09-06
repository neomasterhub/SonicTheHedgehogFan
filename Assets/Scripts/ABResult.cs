using UnityEngine;

public readonly struct ABResult
{
  public readonly Vector2 Normal;
  public readonly float AngleDeg;
  public readonly float AngleRad;

  public ABResult(Vector2 normal, float angleDeg, float angleRad)
  {
    Normal = normal;
    AngleDeg = angleDeg;
    AngleRad = angleRad;
  }
}
