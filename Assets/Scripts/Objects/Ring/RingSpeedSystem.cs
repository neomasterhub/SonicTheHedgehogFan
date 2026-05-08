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
    var physicsModeConfig = _configs.PhysicsModeConfig;

    if (groundDetected)
    {
      var speedYAbs = Mathf.Abs(SpeedY);

      if (speedYAbs < physicsModeConfig.MinBouncingSpeed)
      {
        SpeedY = 0;
        return;
      }

      SpeedY = speedYAbs * physicsModeConfig.BounceFactor;
      return;
    }

    SpeedY -= physicsModeConfig.GravitySpeed;
  }
}
