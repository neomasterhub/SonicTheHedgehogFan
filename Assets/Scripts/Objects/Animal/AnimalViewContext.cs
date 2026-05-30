public readonly struct AnimalViewContext
{
  public readonly bool IsRunning;
  public readonly float SpeedY;

  public AnimalViewContext(bool isRunning, float speedY)
  {
    IsRunning = isRunning;
    SpeedY = speedY;
  }
}
