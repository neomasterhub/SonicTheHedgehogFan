public class AnimalSpeedSystem
{
  public float SpeedX { get; private set; }
  public float SpeedY { get; private set; }

  public void Initialize(float speedX, float speedY)
  {
    SpeedX = speedX;
    SpeedY = speedY;
  }
}
