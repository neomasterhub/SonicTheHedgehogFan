public readonly struct PlayerSpeedInput
{
  public readonly PlayerState PlayerState;
  public readonly PlayerState PrevPlayerState;

  // Sensor Result
  public readonly float DistanceToGround;
  public readonly float GroundAngleRad;

  // Ground
  public readonly float TopSpeed;
  public readonly float FrictionSpeed;
  public readonly float AccelerationSpeed;
  public readonly float DecelerationSpeed;

  // Air
  public readonly float AirTopSpeed;
  public readonly float AirAccelerationSpeed;
  public readonly float MaxFallSpeed;

  // Dead Zones
  public readonly float InputDeadZone;
  public readonly float SkiddingSpeedDeadZone;

  public PlayerSpeedInput(PlayerState playerState, PlayerState prevPlayerState, float distanceToGround, float groundAngleRad, float topSpeed, float frictionSpeed, float accelerationSpeed, float decelerationSpeed, float airTopSpeed, float airAccelerationSpeed, float maxFallSpeed, float inputDeadZone, float skiddingSpeedDeadZone)
  {
    PlayerState = playerState;
    PrevPlayerState = prevPlayerState;
    DistanceToGround = distanceToGround;
    GroundAngleRad = groundAngleRad;
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
