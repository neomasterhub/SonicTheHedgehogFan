public struct PlayerSpeedInput
{
  // Sensor Result
  public float DistanceToGround;
  public float GroundAngleRad;

  // Ground
  public float TopSpeed;
  public float FrictionSpeed;
  public float AccelerationSpeed;
  public float DecelerationSpeed;
  public float SlopeFactor;
  public GroundSide GroundSide;

  // Air
  public float AirTopSpeed;
  public float AirAccelerationSpeed;
  public float GravityUpSpeed;
  public float GravityDownSpeed;
  public float MaxFallSpeed;
  public bool GravityDownEnabled;

  // Dead Zones
  public float InputDeadZone;
  public float SkiddingSpeedDeadZone;
}
