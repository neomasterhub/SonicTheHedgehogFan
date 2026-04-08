public readonly struct PlayerSpeedConfig
{
  public readonly float TopSpeed;
  public readonly float FrictionSpeed;
  public readonly float MaxSkiddingSpeed;
  public readonly float AccelerationSpeed;
  public readonly float DecelerationSpeed;

  public readonly float AirTopSpeed;
  public readonly float AirAccelerationSpeed;
  public readonly float MaxFallSpeed;

  public PlayerSpeedConfig(float topSpeed, float frictionSpeed, float maxSkiddingSpeed, float accelerationSpeed, float decelerationSpeed, float airTopSpeed, float airAccelerationSpeed, float maxFallSpeed)
  {
    TopSpeed = topSpeed;
    FrictionSpeed = frictionSpeed;
    MaxSkiddingSpeed = maxSkiddingSpeed;
    AccelerationSpeed = accelerationSpeed;
    DecelerationSpeed = decelerationSpeed;
    AirTopSpeed = airTopSpeed;
    AirAccelerationSpeed = airAccelerationSpeed;
    MaxFallSpeed = maxFallSpeed;
  }
}
