using UnityEngine;

public class SensorRay
{
  public bool Enabled { get; set; }
  public float Length { get; set; }
  public Color Color { get; set; }
  public Vector2 Direction { get; set; }

  public void Draw(Vector2 source)
  {
    if (Enabled)
    {
      Gizmos.color = Color;
      Gizmos.DrawLine(source, Direction * Length);
    }
  }
}
