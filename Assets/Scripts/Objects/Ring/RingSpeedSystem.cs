public class RingSpeedSystem
{
  public float SpeedX { get; private set; }
  public float SpeedY { get; private set; }

  public void SetSpeed(bool groundDetected)
  {
    if (groundDetected)
    {
      SpeedY = 0.05f;
      return;
    }

    SpeedY -= 0.002f;
  }
}
