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
    var speed = new Vector2(SpeedX, SpeedY);

    if (speed.magnitude < _physicsModeConfig.MinBouncingSpeed)
    {
      SpeedX = 0;
      SpeedY = 0;

      return;
    }

    var reflected = Vector2.Reflect(speed, _context.Normal)
      * _physicsModeConfig.BounceFactor;

    SpeedX = reflected.x;
    SpeedY = reflected.y;
  }
}
