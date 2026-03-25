using System;

public class SensorRaySettings
{
  public Func<bool> Enabled { get; set; }
  public Func<float> Length { get; set; }
}
