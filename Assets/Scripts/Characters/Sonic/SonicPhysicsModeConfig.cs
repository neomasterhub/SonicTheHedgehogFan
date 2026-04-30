public readonly struct SonicPhysicsModeConfig
{
  public readonly float AccelerationSpeed;
  public readonly float DecelerationSpeed;
  public readonly float MinSkiddingSpeed;
  public readonly float FrictionSpeed;
  public readonly float TopSpeed;
  public readonly float AirAccelerationSpeed;
  public readonly float AirTopSpeed;
  public readonly float GravityDownSpeed;
  public readonly float GravityUpSpeed;
  public readonly float MaxFallSpeed;
  public readonly float SlopeFactor;
  public readonly float RollDecelerationSpeed;
  public readonly float RollFrictionSpeed;
  public readonly float RollUphillSlopeFactor;
  public readonly float RollDownhillSlopeFactor;
  public readonly float JumpSpeed;
  public readonly float JumpCutoffSpeed;

  public SonicPhysicsModeConfig(float accelerationSpeed, float decelerationSpeed, float minSkiddingSpeed, float frictionSpeed, float topSpeed, float airAccelerationSpeed, float airTopSpeed, float gravityDownSpeed, float gravityUpSpeed, float maxFallSpeed, float slopeFactor, float rollDecelerationSpeed, float rollFrictionSpeed, float rollUphillSlopeFactor, float rollDownhillSlopeFactor, float jumpSpeed, float jumpCutoffSpeed)
  {
    AccelerationSpeed = accelerationSpeed;
    DecelerationSpeed = decelerationSpeed;
    MinSkiddingSpeed = minSkiddingSpeed;
    FrictionSpeed = frictionSpeed;
    TopSpeed = topSpeed;
    AirAccelerationSpeed = airAccelerationSpeed;
    AirTopSpeed = airTopSpeed;
    GravityDownSpeed = gravityDownSpeed;
    GravityUpSpeed = gravityUpSpeed;
    MaxFallSpeed = maxFallSpeed;
    SlopeFactor = slopeFactor;
    RollDecelerationSpeed = rollDecelerationSpeed;
    RollFrictionSpeed = rollFrictionSpeed;
    RollUphillSlopeFactor = rollUphillSlopeFactor;
    RollDownhillSlopeFactor = rollDownhillSlopeFactor;
    JumpSpeed = jumpSpeed;
    JumpCutoffSpeed = jumpCutoffSpeed;
  }
}
