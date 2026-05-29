public class AnimalSpeedSystem : SpeedSystemBase
{
  private readonly float _speedX;
  private readonly float _jumpSpeed;
  private readonly float _gravitySpeed;

  public AnimalSpeedSystem(float speedX, float jumpSpeed, float gravitySpeed)
  {
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
  }

  private void SetSpeed_Grounded()
  {
    SpeedX = _speedX;
    SpeedY = _jumpSpeed;
  }
}
