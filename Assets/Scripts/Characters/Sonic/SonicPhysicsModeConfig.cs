public class SonicPhysicsModeConfig
{
  public SonicPhysicsModeConfig(float accelerationSpeed, float decelerationSpeed, float minSkiddingSpeed, float frictionSpeed, float minWallSpeed, float minCeilingSpeed, float topSpeed, float airAccelerationSpeed, float airTopSpeed, float gravitySpeed, float maxFallSpeed, float slopeFactor, float rollDecelerationSpeed, float rollFrictionSpeed, float rollUphillSlopeFactor, float rollDownhillSlopeFactor, float rollMinWallSpeed, float rollMinCeilingSpeed, float jumpSpeed, float jumpCutoffSpeed, float hurtSpeedX, float hurtSpeedY, float hurtGravitySpeed, float deathBounceSpeed)
  {
    AccelerationSpeed = accelerationSpeed;
    DecelerationSpeed = decelerationSpeed;
    MinSkiddingSpeed = minSkiddingSpeed;
    FrictionSpeed = frictionSpeed;
    MinWallSpeed = minWallSpeed;
    MinCeilingSpeed = minCeilingSpeed;
    TopSpeed = topSpeed;
    AirAccelerationSpeed = airAccelerationSpeed;
    AirTopSpeed = airTopSpeed;
    GravitySpeed = gravitySpeed;
    MaxFallSpeed = maxFallSpeed;
    SlopeFactor = slopeFactor;
    RollDecelerationSpeed = rollDecelerationSpeed;
    RollFrictionSpeed = rollFrictionSpeed;
    RollUphillSlopeFactor = rollUphillSlopeFactor;
    RollDownhillSlopeFactor = rollDownhillSlopeFactor;
    RollMinWallSpeed = rollMinWallSpeed;
    RollMinCeilingSpeed = rollMinCeilingSpeed;
    JumpSpeed = jumpSpeed;
    JumpCutoffSpeed = jumpCutoffSpeed;
    HurtSpeedX = hurtSpeedX;
    HurtSpeedY = hurtSpeedY;
    HurtGravitySpeed = hurtGravitySpeed;
    DeathBounceSpeed = deathBounceSpeed;
  }

  public float AccelerationSpeed { get; }
  public float DecelerationSpeed { get; }
  public float MinSkiddingSpeed { get; }
  public float FrictionSpeed { get; }
  public float MinWallSpeed { get; }
  public float MinCeilingSpeed { get; }
  public float TopSpeed { get; }
  public float AirAccelerationSpeed { get; }
  public float AirTopSpeed { get; }
  public float GravitySpeed { get; }
  public float MaxFallSpeed { get; }
  public float SlopeFactor { get; }
  public float RollDecelerationSpeed { get; }
  public float RollFrictionSpeed { get; }
  public float RollUphillSlopeFactor { get; }
  public float RollDownhillSlopeFactor { get; }
  public float RollMinWallSpeed { get; }
  public float RollMinCeilingSpeed { get; }
  public float JumpSpeed { get; }
  public float JumpCutoffSpeed { get; }
  public float HurtSpeedX { get; }
  public float HurtSpeedY { get; }
  public float HurtGravitySpeed { get; }
  public float DeathBounceSpeed { get; }
}
