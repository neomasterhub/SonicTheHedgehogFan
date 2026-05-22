/// <summary>
/// Behaviour.
/// </summary>
public partial class GroundSpeedEnemyModuleController
{
  public override void Apply()
  {
    SetSpeed();
  }

  private void SetSpeed()
  {
    if (_context.Ground == null)
    {
      SetSpeed_Airborne();
    }
    else
    {
      SetSpeed_Grounded();
    }
  }

  private void SetSpeed_Airborne()
  {
  }

  private void SetSpeed_Grounded()
  {
  }
}
