public class AnimalSpeedSystem : SpeedSystemBase
{
  private AnimalSpeedContext _context;

  public void SetSpeed(AnimalSpeedContext context)
  {
    _context = context;

    if (context.IsGrounded)
    {
      SetSpeed_Grounded();
    }
    else
    {
      SetSpeed_Airborne();
    }

    RoundSpeed();
  }

  private void SetSpeed_Airborne()
  {
  }

  private void SetSpeed_Grounded()
  {
  }
}
