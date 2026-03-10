public readonly struct PlayerSpeedInput
{
  public readonly PlayerState PlayerState;

  // Sensor Result
  public readonly float DistanceToGround;
  public readonly float GroundAngleRad;

  // Ground
  public readonly float TopSpeed;
  public readonly float FrictionSpeed;
  public readonly float AccelerationSpeed;
  public readonly float DecelerationSpeed;
  public readonly float SlopeFactor;
  public readonly GroundSide GroundSide;

  // Air
  public readonly float AirTopSpeed;
  public readonly float AirAccelerationSpeed;
  public readonly float GravityUpSpeed;
  public readonly float GravityDownSpeed;
  public readonly float MaxFallSpeed;
  public readonly bool GravityDownEnabled;

  // Dead Zones
  public readonly float InputDeadZone;
  public readonly float SkiddingSpeedDeadZone;

  public PlayerSpeedInput(PlayerState playerState, float distanceToGround, float groundAngleRad, float topSpeed, float frictionSpeed, float accelerationSpeed, float decelerationSpeed, float slopeFactor, GroundSide groundSide, float airTopSpeed, float airAccelerationSpeed, float gravityUpSpeed, float gravityDownSpeed, float maxFallSpeed, bool gravityDownEnabled, float inputDeadZone, float skiddingSpeedDeadZone)
  {
    PlayerState = playerState;
    DistanceToGround = distanceToGround;
    GroundAngleRad = groundAngleRad;
    TopSpeed = topSpeed;
    FrictionSpeed = frictionSpeed;
    AccelerationSpeed = accelerationSpeed;
    DecelerationSpeed = decelerationSpeed;
    SlopeFactor = slopeFactor;
    GroundSide = groundSide;
    AirTopSpeed = airTopSpeed;
    AirAccelerationSpeed = airAccelerationSpeed;
    GravityUpSpeed = gravityUpSpeed;
    GravityDownSpeed = gravityDownSpeed;
    MaxFallSpeed = maxFallSpeed;
    GravityDownEnabled = gravityDownEnabled;
    InputDeadZone = inputDeadZone;
    SkiddingSpeedDeadZone = skiddingSpeedDeadZone;
  }
}
