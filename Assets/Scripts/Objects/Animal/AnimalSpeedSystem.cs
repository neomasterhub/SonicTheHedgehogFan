using static AnimalConsts;

public class AnimalSpeedSystem : SpeedSystemBase
{
  private float _speedX;
  private float _jumpSpeed;
  private float _gravitySpeed;

  public void Initialize(float speedX, float speedY, float jumpSpeed, float gravitySpeed)
  {
    Initialize(0, speedY);

    _speedX = speedX;
    _jumpSpeed = jumpSpeed;
    _gravitySpeed = gravitySpeed;
  }

  public void SetSpeed(AnimalSpeedContext context)
  {
    if (context.IsGrounded)
    {
      SetSpeed_Grounded();
    }
    else
    {
      SetSpeed_Airborne();
    }
  }

  private void SetSpeed_Airborne()
  {
    SpeedY -= _gravitySpeed;

    if (SpeedY < -MaxFallSpeed)
    {
      SpeedY = -MaxFallSpeed;
    }
  }

  private void SetSpeed_Grounded()
  {
    SpeedX = _speedX;
    SpeedY = _jumpSpeed;
  }
}
