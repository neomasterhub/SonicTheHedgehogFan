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

  public void SetSpeed(bool groundDetected)
  {
    if (groundDetected)
    {
      SpeedY = Mathf.Abs(SpeedY) * _configs.PhysicsModeConfig.BounceFactor;
      return;
    }

    SpeedY -= _configs.PhysicsModeConfig.GravitySpeed;
  }
}
