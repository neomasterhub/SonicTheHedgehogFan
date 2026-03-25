using UnityEngine;

public class UDFSensor
{
  public bool Enabled { get; set; }
  public float Radius { get; set; }
  public Color EnabledColor { get; set; }
  public Color? DisabledColor { get; set; }
  public Vector2 Position { get; set; }
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
