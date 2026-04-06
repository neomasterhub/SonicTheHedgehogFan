public readonly struct SonicViewRotatorContext
{
  public readonly bool IsGrounded;
  public readonly float GroundSpeed;
  public readonly float GroundAngleDeg;
  public readonly GroundSide GroundSide;
  public readonly GroundSide PrevGroundSide;

  public SonicViewRotatorContext(bool isGrounded, float groundSpeed, float groundAngleDeg, GroundSide groundSide, GroundSide prevGroundSide)
  {
    IsGrounded = isGrounded;
    GroundSpeed = groundSpeed;
    GroundAngleDeg = groundAngleDeg;
    GroundSide = groundSide;
    PrevGroundSide = prevGroundSide;
  }
}
