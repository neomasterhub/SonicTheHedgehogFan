using UnityEngine;

public class UDSensor : SensorBase
{
  public UDSensor(
    Color enabledColor,
    Vector2 localPosition,
    Vector2 upRayDirection,
    Vector2 downRayDirection)
  {
    EnabledColor = enabledColor;
    LocalPosition = localPosition;
    Position = localPosition;
    UpRay = new(enabledColor, Position, upRayDirection);
    DownRay = new(enabledColor, Position, downRayDirection);
  }

  public SensorRay UpRay { get; }
  public SensorRay DownRay { get; }

  public override void SetParentPosition(Vector2 parentPosition)
  {
    base.SetParentPosition(parentPosition);
    UpRay.Origin = Position;
    DownRay.Origin = Position;
  }

  public override void Draw()
  {
    if (Enabled)
    {
      UpRay.Draw();
      DownRay.Draw();
      DrawEnabled();
    }
    else
    {
      DrawDisabled();
    }
  }
}
