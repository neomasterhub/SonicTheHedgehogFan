using System;

/// <summary>
/// Behaviour.
/// </summary>
public partial class GroundSpeedBlockModuleController
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
    _context.SpeedX = _context.Speed;
    _context.SpeedY -= _gravitySpeed;

    if (_context.SpeedY < -_maxFallSpeed)
    {
      _context.SpeedY = -_maxFallSpeed;
    }
  }

  private void SetSpeed_Grounded()
  {
    if (_context.Speed == 0)
    {
      _context.SpeedX = 0;
      _context.SpeedY = 0;

      return;
    }

    var angleRad = _context.Ground.Value.AngleRad;
    _context.SpeedX = _context.Speed * MathF.Cos(angleRad);
    _context.SpeedY = _context.Speed * MathF.Sin(angleRad);
  }
}
