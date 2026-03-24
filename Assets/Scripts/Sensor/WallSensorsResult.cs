public struct WallSensorsResult
{
  public bool WallDetected;
  public float Distance;

  public void Reset()
  {
    WallDetected = false;
    Distance = float.PositiveInfinity;
  }
}
