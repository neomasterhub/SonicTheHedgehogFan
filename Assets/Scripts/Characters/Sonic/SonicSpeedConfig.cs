public readonly struct SonicSpeedConfig
{
  public readonly float TopSpeed;
  public readonly float FrictionSpeed;
  public readonly float MaxSkiddingSpeed;
  public readonly float AccelerationSpeed;
  public readonly float DecelerationSpeed;

  public readonly float AirTopSpeed;
  public readonly float AirAccelerationSpeed;
  public readonly float MaxFallSpeed;

  public readonly float RollFrictionSpeed;
  public readonly float RollDecelerationSpeed;

  public SonicSpeedConfig(float topSpeed, float frictionSpeed, float maxSkiddingSpeed, float accelerationSpeed, float decelerationSpeed, float airTopSpeed, float airAccelerationSpeed, float maxFallSpeed, float rollFrictionSpeed, float rollDecelerationSpeed)
  {
    TopSpeed = topSpeed;
    FrictionSpeed = frictionSpeed;
    MaxSkiddingSpeed = maxSkiddingSpeed;
    AccelerationSpeed = accelerationSpeed;
    DecelerationSpeed = decelerationSpeed;
    AirTopSpeed = airTopSpeed;
    AirAccelerationSpeed = airAccelerationSpeed;
    MaxFallSpeed = maxFallSpeed;
    RollFrictionSpeed = rollFrictionSpeed;
    RollDecelerationSpeed = rollDecelerationSpeed;
  }
}
