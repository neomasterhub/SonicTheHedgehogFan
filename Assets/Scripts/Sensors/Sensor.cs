using UnityEngine;

public class Sensor : SensorBase
{
  public Sensor(
    Color enabledColor,
    Vector2 localPosition,
    Vector2 rayPosition)
  {
    EnabledColor = enabledColor;
    LocalPosition = localPosition;
    Position = localPosition;
    Ray = new(enabledColor, Position, rayPosition);
  }

  public SensorRay Ray { get; }

  public override void SetParentPosition(Vector2 parentPosition)
  {
    base.SetParentPosition(parentPosition);
    Ray.Origin = Position;
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
