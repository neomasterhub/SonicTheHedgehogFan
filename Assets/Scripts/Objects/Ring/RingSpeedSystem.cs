using UnityEngine;
using static SharedConsts.Physics;

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

  private void RoundSpeed()
  {
    SpeedX = SpeedX.Round(SpeedRoundingDigits);
    SpeedY = SpeedY.Round(SpeedRoundingDigits);
  }

  public void Initialize(float speedX, float speedY)
  {
    SpeedX = speedX;
    SpeedY = speedY;
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
  }
}
