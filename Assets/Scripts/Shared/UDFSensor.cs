using UnityEngine;

public class UDFSensor : SensorBase, ISensor
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

  public void SetParentPosition(Vector2 parentPosition)
  {
    ParentPosition = parentPosition;
    Position = parentPosition + LocalPosition;
    UpRay.Origin = Position;
    DownRay.Origin = Position;
    FrontRay.Origin = Position;
  }

  public void Draw()
  {
    if (Enabled)
    {
      UpRay.Draw();
      DownRay.Draw();
      FrontRay.Draw();

      Gizmos.color = EnabledColor;
      Gizmos.DrawSphere(Position, Radius);
    }
    else if (DisabledColor.HasValue)
    {
      Gizmos.color = DisabledColor.Value;
      Gizmos.DrawSphere(Position, Radius);
    }
  }
}
