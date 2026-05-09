public class RingPhysicsModeConfig
{
  public RingPhysicsModeConfig(float gravitySpeed, float bounceFactor)
  {
    GravitySpeed = gravitySpeed;
    BounceFactor = bounceFactor;
  }

  public float GravitySpeed { get; }
  public float BounceFactor { get; }
}
