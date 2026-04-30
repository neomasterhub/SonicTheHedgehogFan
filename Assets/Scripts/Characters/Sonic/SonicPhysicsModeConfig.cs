public class SonicPhysicsModeConfig
{
  public SonicPhysicsModeConfig(float accelerationSpeed, float decelerationSpeed, float minSkiddingSpeed, float frictionSpeed, float topSpeed, float airAccelerationSpeed, float airTopSpeed, float gravityDownSpeed, float gravityUpSpeed, float maxFallSpeed, float slopeFactor, float rollDecelerationSpeed, float rollFrictionSpeed, float rollUphillSlopeFactor, float rollDownhillSlopeFactor, float jumpSpeed, float jumpCutoffSpeed)
  {
    AccelerationSpeed = accelerationSpeed;
    DecelerationSpeed = decelerationSpeed;
    MinSkiddingSpeed = minSkiddingSpeed;
    FrictionSpeed = frictionSpeed;
    TopSpeed = topSpeed;
    AirAccelerationSpeed = airAccelerationSpeed;
    AirTopSpeed = airTopSpeed;
    GravitySpeed = new(gravityUpSpeed, gravityDownSpeed);
    MaxFallSpeed = maxFallSpeed;
    SlopeFactor = slopeFactor;
    RollDecelerationSpeed = rollDecelerationSpeed;
    RollFrictionSpeed = rollFrictionSpeed;
    RollUphillSlopeFactor = rollUphillSlopeFactor;
    RollDownhillSlopeFactor = rollDownhillSlopeFactor;
    JumpSpeed = jumpSpeed;
    JumpCutoffSpeed = jumpCutoffSpeed;
  }

  public float AccelerationSpeed { get; }
  public float DecelerationSpeed { get; }
  public float MinSkiddingSpeed { get; }
  public float FrictionSpeed { get; }
  public float TopSpeed { get; }
  public float AirAccelerationSpeed { get; }
  public float AirTopSpeed { get; }
  public float MaxFallSpeed { get; }
  public float SlopeFactor { get; }
  public float RollDecelerationSpeed { get; }
  public float RollFrictionSpeed { get; }
  public float RollUphillSlopeFactor { get; }
  public float RollDownhillSlopeFactor { get; }
  public float JumpSpeed { get; }
  public float JumpCutoffSpeed { get; }
  public GravitySpeed GravitySpeed { get; }
}
