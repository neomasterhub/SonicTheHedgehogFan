using UnityEngine;

public class SensorRay
{
  public SensorRay()
  {
  }

  public SensorRay(Color color, Vector2 origin, Vector2 direction)
  {
    Color = color;
    Origin = origin;
    Direction = direction;
  }

  public bool Enabled { get; set; } = true;
  public float Length { get; set; } = 1;
  public Color Color { get; set; } = Color.red;
  public Vector2 Origin { get; set; }
  public Vector2 Direction { get; set; } = Vector2.right;

  public RaycastHit2D? Cast(LayerMask layer)
  {
    if (!Enabled)
    {
      return null;
    }

    var hit = Physics2D.Raycast(Origin, Direction, Length, layer);

    return hit.collider == null ? null : hit;
  }

  public void Draw()
  {
    if (Enabled)
    {
      Gizmos.color = Color;
      Gizmos.DrawLine(Origin, Origin + (Direction * Length));
    }
  }
}
