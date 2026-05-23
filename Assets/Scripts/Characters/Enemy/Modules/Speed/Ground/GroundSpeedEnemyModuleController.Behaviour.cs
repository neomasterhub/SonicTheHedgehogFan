using System;

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
    if (_context.IsStatic)
    {
      return;
    }

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
    var angleRad = _context.Ground.Value.AngleRad;
    _context.SpeedX = _context.AccelerationSpeed * MathF.Cos(angleRad);
    _context.SpeedY = _context.AccelerationSpeed * MathF.Sin(angleRad);
  }
}
