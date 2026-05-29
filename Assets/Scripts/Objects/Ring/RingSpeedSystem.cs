using UnityEngine;
using static RingConsts.Physics;

public class RingSpeedSystem : SpeedSystemBase
{
  private readonly RingConfigs _configs;

  private RingSpeedContext _context;
  private RingPhysicsModeConfig _physicsModeConfig;

  public RingSpeedSystem(RingConfigs configs)
  {
    _configs = configs;
  }

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

    RoundSpeed();
  }

  private void SetSpeed_Airborne()
  {
    SpeedY -= _physicsModeConfig.GravitySpeed;
  }

  private void SetSpeed_Grounded()
  {
    if (SpeedX == 0 && SpeedY == 0)
    {
      return;
    }

    var speed = Vector2.Reflect(new(SpeedX, SpeedY), _context.Normal)
      * _physicsModeConfig.BounceFactor;

    SpeedX = speed.x;
    SpeedY = speed.y;

    if (Mathf.Abs(SpeedX) <= MaxStopSpeed)
    {
      SpeedX = 0;
    }

    if (Mathf.Abs(SpeedY) <= MaxStopSpeed)
    {
      SpeedY = 0;
    }
  }
}
