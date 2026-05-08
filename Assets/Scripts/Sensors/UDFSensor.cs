using UnityEngine;

public class UDFSensor : SensorBase
{
  public UDFSensor(
    Color enabledColor,
    Vector2 localPosition,
    Vector2 upRayDirection,
    Vector2 downRayDirection,
    Vector2 frontRayDirection)
  {
    EnabledColor = enabledColor;
    LocalPosition = localPosition;
    Position = localPosition;
    UpRay = new(enabledColor, Position, upRayDirection);
    DownRay = new(enabledColor, Position, downRayDirection);
    FrontRay = new(enabledColor, Position, frontRayDirection);
  }

  public SensorRay UpRay { get; }
  public SensorRay DownRay { get; }
  public SensorRay FrontRay { get; }

  public override void SetParentPosition(Vector2 parentPosition)
  {
    base.SetParentPosition(parentPosition);
    UpRay.Origin = Position;
    DownRay.Origin = Position;
    FrontRay.Origin = Position;
  }

  public override void Draw()
  {
    if (Enabled)
    {
      UpRay.Draw();
      DownRay.Draw();
      FrontRay.Draw();
      DrawEnabled();
    }
    else
    {
      DrawDisabled();
    }
  }
}
