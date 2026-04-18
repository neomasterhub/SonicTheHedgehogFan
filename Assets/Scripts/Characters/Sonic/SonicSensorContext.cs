using UnityEngine;

public readonly struct SonicSensorContext
{
  public readonly SonicSizeMode SizeMode;
  public readonly GroundSide GroundSide;
  public readonly Vector2 ParentPosition;
  public readonly SonicSensorChecks SensorChecks;
  public readonly SonicSensorRayLengths SensorRayLengths;

  public SonicSensorContext(SonicSizeMode sizeMode, GroundSide groundSide, Vector2 parentPosition, SonicSensorChecks sensorChecks, SonicSensorRayLengths sensorRayLengths)
  {
    SizeMode = sizeMode;
    GroundSide = groundSide;
    ParentPosition = parentPosition;
    SensorChecks = sensorChecks;
    SensorRayLengths = sensorRayLengths;
  }
}
