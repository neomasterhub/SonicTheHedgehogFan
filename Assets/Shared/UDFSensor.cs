using UnityEngine;

public class UDFSensor
{
  public UDFSensor()
  {
  }

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
  public Vector2 Position { get; set; }
  public Vector2 LocalPosition { get; set; }
  public SensorRay UpRay { get; set; }
  public SensorRay DownRay { get; set; }
  public SensorRay FrontRay { get; set; }

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
