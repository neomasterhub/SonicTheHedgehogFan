using static SharedConsts.Physics;

public abstract class SpeedSystemBase
{
  public float SpeedX { get; protected set; }
  public float SpeedY { get; protected set; }

  public virtual void Initialize(float speedX, float speedY)
  {
    SpeedX = speedX;
    SpeedY = speedY;
  }

  public virtual void RoundSpeed()
  {
    SpeedX = SpeedX.Round(SpeedRoundingDigits);
    SpeedY = SpeedY.Round(SpeedRoundingDigits);
  }
}
