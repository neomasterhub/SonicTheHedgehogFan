public class PlayerSpeedContext
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

  public PlayerSpeedContext(float topSpeed, float frictionSpeed, float accelerationSpeed, float decelerationSpeed, float airTopSpeed, float airAccelerationSpeed, float maxFallSpeed, float inputDeadZone, float skiddingSpeedDeadZone, bool isGrounded, bool prevIsGrounded, float? groundAngleRad, float? distanceToGround)
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
    IsGrounded = isGrounded;
    PrevIsGrounded = prevIsGrounded;
    GroundAngleRad = groundAngleRad;
    DistanceToGround = distanceToGround;
  }

  public bool IsGrounded { get; private set; }
  public bool PrevIsGrounded { get; private set; }
  public float? GroundAngleRad { get; private set; }
  public float? DistanceToGround { get; private set; }

  public void UpdateGrounded(bool prevIsGrounded, float groundAngleRad, float distanceToGround)
  {
    IsGrounded = true;
    PrevIsGrounded = prevIsGrounded;
    GroundAngleRad = groundAngleRad;
    DistanceToGround = distanceToGround;
  }
}
