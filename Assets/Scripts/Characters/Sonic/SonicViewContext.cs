public readonly struct SonicViewContext
{
  public readonly bool IsGrounded;
  public readonly bool IsSkidding;
  public readonly bool IsBalancing;
  public readonly float SpeedX;
  public readonly float GroundSpeed;
  public readonly float GroundAngleDeg;
  public readonly GroundSide PrevGroundSide;

  public SonicViewContext(bool isGrounded, bool isSkidding, bool isBalancing, float speedX, float groundSpeed, float groundAngleDeg, GroundSide prevGroundSide)
  {
    IsGrounded = isGrounded;
    IsSkidding = isSkidding;
    IsBalancing = isBalancing;
    SpeedX = speedX;
    GroundSpeed = groundSpeed;
    GroundAngleDeg = groundAngleDeg;
    PrevGroundSide = prevGroundSide;
  }
}
