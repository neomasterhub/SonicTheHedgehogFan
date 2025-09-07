using UnityEngine;

public struct ABResult
{
  public Vector2 Contact;
  public Vector2 Normal;
  public float AngleDeg;
  public float AngleRad;
  public bool GroundDetected;

  public void Reset()
  {
    Contact = Vector2.zero;
    Normal = Vector2.zero;
    AngleDeg = 0;
    AngleRad = 0;
    GroundDetected = false;
  }

  public void Draw(
    float length = 1,
    float beginRadius = 0,
    float endRadius = 0,
    Color? color = null)
  {
    var begin = Contact;
    var end = Contact + (Normal * length);

    Gizmos.color = color ?? Color.yellow;
    Gizmos.DrawLine(begin, end);
    Gizmos.DrawSphere(begin, beginRadius);
    Gizmos.DrawSphere(end, endRadius);
  }
}
