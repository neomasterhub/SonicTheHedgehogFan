using UnityEngine;
using static SharedConsts.Rendering;

public abstract class SensorBase : ISensor
{
  protected IMeshRenderer _meshRenderer;

  public bool Enabled { get; set; } = true;
  public float Radius { get; set; } = SensorOriginRadius;
  public Color EnabledColor { get; set; } = Color.red;
  public Color? DisabledColor { get; set; } = Color.darkRed;
  public Vector2 Position { get; protected set; }
  public Vector2 LocalPosition { get; protected set; }
  public Vector2 ParentPosition { get; protected set; }

  public virtual void SetLocalPosition(Vector2 localPosition)
  {
    LocalPosition = localPosition;
    Position = ParentPosition + localPosition;
  }

  public virtual void SetParentPosition(Vector2 parentPosition)
  {
    ParentPosition = parentPosition;
    Position = parentPosition + LocalPosition;
  }

  public virtual void SetMeshRenderer(IMeshRenderer meshRenderer)
  {
    _meshRenderer = meshRenderer;
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
    _meshRenderer.DrawSquare(Position, Radius, EnabledColor);
  }

  protected void DrawDisabled()
  {
    if (DisabledColor.HasValue)
    {
      _meshRenderer.DrawSquare(Position, Radius, DisabledColor.Value);
    }
  }
}
