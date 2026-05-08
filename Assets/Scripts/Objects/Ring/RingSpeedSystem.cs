using UnityEngine;

public class RingSpeedSystem
{
  private readonly RingConfigs _configs;

  private RingSpeedContext _context;
  private RingPhysicsModeConfig _physicsModeConfig;

  public RingSpeedSystem(RingConfigs configs)
  {
    _configs = configs;
  }

  public float SpeedX { get; private set; }
  public float SpeedY { get; private set; }

  public void SetSpeed(RingSpeedContext context)
  {
    _context = context;
    _physicsModeConfig = _configs.PhysicsModeConfig;

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
    SpeedY -= _physicsModeConfig.GravitySpeed;
  }

  private void SetSpeed_Grounded()
  {
    var speed = Mathf.Sqrt((SpeedX * SpeedX) + (SpeedY * SpeedY));

    if (speed < _physicsModeConfig.MinBouncingSpeed)
    {
      SpeedY = 0;
      return;
    }

    var bounceSpeed = speed * _physicsModeConfig.BounceFactor;
    SpeedX = -bounceSpeed * Mathf.Sin(_context.GroundAngleRad.Value);
    SpeedY = bounceSpeed * Mathf.Cos(_context.GroundAngleRad.Value);
  }
}
