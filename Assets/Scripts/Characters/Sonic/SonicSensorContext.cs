using UnityEngine;

public readonly struct SonicSensorContext
{
  public readonly SonicSizeMode SizeMode;
  public readonly GroundSide GroundSide;
  public readonly Vector2 ParentPosition;
  public readonly SonicSensorFlags SensorFlags;
  public readonly SonicSensorRayLengths SensorRayLengths;

  public SonicSensorContext(SonicSizeMode sizeMode, GroundSide groundSide, Vector2 parentPosition, SonicSensorFlags sensorFlags, SonicSensorRayLengths sensorRayLengths)
  {
    SizeMode = sizeMode;
    GroundSide = groundSide;
    ParentPosition = parentPosition;
    SensorFlags = sensorFlags;
    SensorRayLengths = sensorRayLengths;
  }
}
