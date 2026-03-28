public readonly struct PlayerSpeedConfig
{
  public readonly float TopSpeed;
  public readonly float FrictionSpeed;
  public readonly float AccelerationSpeed;
  public readonly float DecelerationSpeed;

  public readonly float AirTopSpeed;
  public readonly float AirAccelerationSpeed;
  public readonly float MaxFallSpeed;

  public readonly float InputDeadZone;
  public readonly float SkiddingSpeedDeadZone;

  public PlayerSpeedConfig(float topSpeed, float frictionSpeed, float accelerationSpeed, float decelerationSpeed, float airTopSpeed, float airAccelerationSpeed, float maxFallSpeed, float inputDeadZone, float skiddingSpeedDeadZone)
  {
    TopSpeed = topSpeed;
    FrictionSpeed = frictionSpeed;
    AccelerationSpeed = accelerationSpeed;
    DecelerationSpeed = decelerationSpeed;
    AirTopSpeed = airTopSpeed;
    AirAccelerationSpeed = airAccelerationSpeed;
    MaxFallSpeed = maxFallSpeed;
    InputDeadZone = inputDeadZone;
    SkiddingSpeedDeadZone = skiddingSpeedDeadZone;
  }
}
