using UnityEngine;

public abstract class SensorBase
{
  public bool Enabled { get; set; } = true;
  public float Radius { get; set; } = 0.03f;
  public Color EnabledColor { get; set; } = Color.red;
  public Color? DisabledColor { get; set; } = Color.darkRed;
  public Vector2 Position { get; protected set; }
  public Vector2 LocalPosition { get; protected set; }
  public Vector2 ParentPosition { get; protected set; }
}
