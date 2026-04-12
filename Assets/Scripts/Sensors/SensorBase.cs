using UnityEngine;

public abstract class SensorBase : ISensor
{
  public bool Enabled { get; set; } = true;
  public float Radius { get; set; } = 0.03f;
  public Color EnabledColor { get; set; } = Color.red;
  public Color? DisabledColor { get; set; } = Color.darkRed;
  public Vector2 Position { get; protected set; }
  public Vector2 LocalPosition { get; protected set; }
  public Vector2 ParentPosition { get; protected set; }

  public virtual void SetParentPosition(Vector2 parentPosition)
  {
    ParentPosition = parentPosition;
    Position = parentPosition + LocalPosition;
  }

  public virtual void Draw()
  {
    if (Enabled)
    {
      DrawEnabled();
    }
    else
    {
      DrawDisabled();
    }
  }

  protected void DrawEnabled()
  {
    Gizmos.color = EnabledColor;
    Gizmos.DrawSphere(Position, Radius);
  }

  protected void DrawDisabled()
  {
    if (DisabledColor.HasValue)
    {
      Gizmos.color = DisabledColor.Value;
      Gizmos.DrawSphere(Position, Radius);
    }
  }
}
