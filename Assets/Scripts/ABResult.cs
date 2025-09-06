using UnityEngine;

public readonly struct ABResult
{
  public readonly Vector2 Contact;
  public readonly Vector2 Normal;
  public readonly float AngleDeg;
  public readonly float AngleRad;

  public ABResult(Vector2 contact, Vector2 normal, float angleDeg, float angleRad)
  {
    Contact = contact;
    Normal = normal;
    AngleDeg = angleDeg;
    AngleRad = angleRad;
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
