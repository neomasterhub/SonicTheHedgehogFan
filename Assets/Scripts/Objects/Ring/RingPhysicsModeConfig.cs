public class RingPhysicsModeConfig
{
  public RingPhysicsModeConfig(float gravitySpeed, float bounceFactor, float minBouncingSpeed)
  {
    GravitySpeed = gravitySpeed;
    BounceFactor = bounceFactor;
    MinBouncingSpeed = minBouncingSpeed;
  }

  public float GravitySpeed { get; }
  public float BounceFactor { get; }
  public float MinBouncingSpeed { get; }
}
