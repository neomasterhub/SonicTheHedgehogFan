using UnityEngine;

public class UDFSensor
{
  public bool Enabled { get; set; }
  public Color EnabledColor { get; set; }
  public Color? DisabledColor { get; set; }
  public Vector2 Position { get; set; }
  public SensorRay UpRay { get; set; }
  public SensorRay DownRay { get; set; }
  public SensorRay FrontRay { get; set; }
}
