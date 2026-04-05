public readonly struct SonicViewRotatorContext
{
  public readonly bool IsGrounded;
  public readonly float GroundSpeed;
  public readonly float GroundAngleDeg;

  public SonicViewRotatorContext(bool isGrounded, float groundSpeed, float groundAngleDeg)
  {
    IsGrounded = isGrounded;
    GroundSpeed = groundSpeed;
    GroundAngleDeg = groundAngleDeg;
  }
}
