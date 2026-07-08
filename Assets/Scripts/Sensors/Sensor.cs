using UnityEngine;

public class Sensor : SensorBase
{
  public Sensor(
    Color enabledColor,
    Vector2 localPosition,
    Vector2 rayDirection)
  {
    EnabledColor = enabledColor;
    LocalPosition = localPosition;
    Position = localPosition;
    Ray = new(enabledColor, Position, rayDirection);
  }

  public SensorRay Ray { get; }

  public override void SetParentPosition(Vector2 parentPosition)
  {
    base.SetParentPosition(parentPosition);
    Ray.Origin = Position;
  }

  public override void SetMeshRenderer(IMeshRenderer meshRenderer)
  {
    base.SetMeshRenderer(meshRenderer);
    Ray.SetMeshRenderer(meshRenderer);
  }

  public override void Draw()
  {
    if (Enabled)
    {
      Ray.Draw();
      DrawEnabled();
    }
    else
    {
      DrawDisabled();
    }
  }
}
