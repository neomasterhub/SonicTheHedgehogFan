using UnityEngine;

public class UDFSensor
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
    UpRay = new(enabledColor, upRayPosition);
    DownRay = new(enabledColor, downRayPosition);
    FrontRay = new(enabledColor, frontRayPosition);
  }

  public bool Enabled { get; set; }
  public float Radius { get; set; }
  public Color EnabledColor { get; set; }
  public Color? DisabledColor { get; set; }
  public Vector2 Position { get; private set; }
  public Vector2 LocalPosition { get; private set; }
  public Vector2 ParentPosition { get; private set; }
  public SensorRay UpRay { get; }
  public SensorRay DownRay { get; }
  public SensorRay FrontRay { get; }

  public void SetParentPosition(Vector2 parentPosition)
  {
    ParentPosition = parentPosition;
    Position = parentPosition + LocalPosition;
  }

  public void Draw()
  {
    if (Enabled)
    {
      UpRay.Draw(Position);
      DownRay.Draw(Position);
      FrontRay.Draw(Position);

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
