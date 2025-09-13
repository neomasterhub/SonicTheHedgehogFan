public struct PlayerSpeedInput
{
  // Sensor Result
  public float DistanceToGround;
  public float GroundAngleRad;
  public float GroundSensorLength;

  // Ground
  public float TopSpeed;
  public float FrictionSpeed;
  public float AccelerationSpeed;
  public float DecelerationSpeed;

  // Air
  public float AirAccelerationSpeed;
  public float AirTopSpeed;
  public float GravityUpSpeed;
  public float GravityDownSpeed;
  public float MaxFallSpeed;

  // Dead Zones
  public float GroundSpeedDeadZone;
  public float InputDeadZone;
}
