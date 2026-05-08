using UnityEngine;

public class RingSpeedSystem
{
  private readonly RingConfigs _configs;

  public RingSpeedSystem(RingConfigs configs)
  {
    _configs = configs;
  }

  public float SpeedX { get; private set; }
  public float SpeedY { get; private set; }

  public void SetSpeed(RingSpeedContext context)
  {
    var physicsModeConfig = _configs.PhysicsModeConfig;

    if (context.IsGrounded)
    {
      var speed = Mathf.Sqrt((SpeedX * SpeedX) + (SpeedY * SpeedY));

      if (speed < physicsModeConfig.MinBouncingSpeed)
      {
        SpeedY = 0;
        return;
      }

      var bounceSpeed = speed * physicsModeConfig.BounceFactor;
      SpeedX = -bounceSpeed * Mathf.Sin(context.GroundAngleRad.Value);
      SpeedY = bounceSpeed * Mathf.Cos(context.GroundAngleRad.Value);

      return;
    }

    SpeedY -= physicsModeConfig.GravitySpeed;
  }
}
