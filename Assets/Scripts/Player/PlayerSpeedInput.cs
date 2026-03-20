using UnityEngine;

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
  public readonly float SlopeFactor;

  // Air
  public readonly bool GravityEnabled;
  public readonly float AirTopSpeed;
  public readonly float AirAccelerationSpeed;
  public readonly float GravityUpSpeed;
  public readonly float GravityDownSpeed;
  public readonly float MaxFallSpeed;
  public readonly Vector2 WallToAirSpeedDelta;

  // Dead Zones
  public readonly float InputDeadZone;
  public readonly float SkiddingSpeedDeadZone;

  public PlayerSpeedInput(PlayerState playerState, PlayerState prevPlayerState, float distanceToGround, float groundAngleRad, float topSpeed, float frictionSpeed, float accelerationSpeed, float decelerationSpeed, float slopeFactor, bool gravityEnabled, float airTopSpeed, float airAccelerationSpeed, float gravityUpSpeed, float gravityDownSpeed, float maxFallSpeed, Vector2 wallToAirSpeedDelta, float inputDeadZone, float skiddingSpeedDeadZone)
  {
    PlayerState = playerState;
    PrevPlayerState = prevPlayerState;
    DistanceToGround = distanceToGround;
    GroundAngleRad = groundAngleRad;
    TopSpeed = topSpeed;
    FrictionSpeed = frictionSpeed;
    AccelerationSpeed = accelerationSpeed;
    DecelerationSpeed = decelerationSpeed;
    SlopeFactor = slopeFactor;
    GravityEnabled = gravityEnabled;
    AirTopSpeed = airTopSpeed;
    AirAccelerationSpeed = airAccelerationSpeed;
    GravityUpSpeed = gravityUpSpeed;
    GravityDownSpeed = gravityDownSpeed;
    MaxFallSpeed = maxFallSpeed;
    WallToAirSpeedDelta = wallToAirSpeedDelta;
    InputDeadZone = inputDeadZone;
    SkiddingSpeedDeadZone = skiddingSpeedDeadZone;
  }
}
