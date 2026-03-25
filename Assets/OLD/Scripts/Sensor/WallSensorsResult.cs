using UnityEngine;

public struct WallSensorsResult
{
  public bool WallDetected;
  public float Distance;

  public void Reset()
  {
    WallDetected = false;
    Distance = float.PositiveInfinity;
  }

  public void Set(
    RaycastHit2D hit,
    float sensorLength)
  {
    WallDetected = hit.distance <= sensorLength;
  }
}
