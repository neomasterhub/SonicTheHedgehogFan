public record Timer
{
  public Timer()
  {
  }

  public Timer(float remainingTime)
  {
    RemainingTime = remainingTime;
  }

  public float RemainingTime { get; set; }
}
