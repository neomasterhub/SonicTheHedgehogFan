public readonly struct SonicViewContext
{
  public readonly bool IsGrounded;
  public readonly bool IsSkidding;
  public readonly bool IsBalancing;
  public readonly float SpeedX;
  public readonly GroundSide PrevGroundSide;

  public SonicViewContext(bool isGrounded, bool isSkidding, bool isBalancing, float speedX, GroundSide prevGroundSide)
  {
    IsGrounded = isGrounded;
    IsSkidding = isSkidding;
    IsBalancing = isBalancing;
    SpeedX = speedX;
    PrevGroundSide = prevGroundSide;
  }
}
