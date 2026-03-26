using UnityEngine;

public class SensorRay
{
  public SensorRay()
  {
  }

  public SensorRay(Color color, Vector2 direction)
  {
    Color = color;
    Direction = direction;
  }

  public bool Enabled { get; set; } = true;
  public float Length { get; set; } = 1;
  public Color Color { get; set; } = Color.red;
  public Vector2 Direction { get; set; } = Vector2.right;

  public void Draw(Vector2 origin)
  {
    if (Enabled)
    {
      Gizmos.color = Color;
      Gizmos.DrawLine(origin, origin + (Direction * Length));
    }
  }
}
