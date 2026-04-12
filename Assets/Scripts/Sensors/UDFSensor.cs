using UnityEngine;

public class UDFSensor : SensorBase
{
  public UDFSensor(
    Color enabledColor,
    Vector2 localPosition,
    Vector2 upRayPosition,
    Vector2 downRayPosition,
    Vector2 frontRayPosition)
  {
    EnabledColor = enabledColor;
    LocalPosition = localPosition;
    Position = localPosition;
    UpRay = new(enabledColor, Position, upRayPosition);
    DownRay = new(enabledColor, Position, downRayPosition);
    FrontRay = new(enabledColor, Position, frontRayPosition);
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
