public readonly struct SonicViewRotatorContext
{
  public readonly bool IsGrounded;
  public readonly bool IsZeroGroundSpeedProgressReached;
  public readonly float GroundSpeed;
  public readonly float GroundAngleDeg;
  public readonly GroundSide GroundSide;
  public readonly GroundSide PrevGroundSide;

  public SonicViewRotatorContext(bool isGrounded, bool isZeroGroundSpeedProgressReached, float groundSpeed, float groundAngleDeg, GroundSide groundSide, GroundSide prevGroundSide)
  {
    IsGrounded = isGrounded;
    IsZeroGroundSpeedProgressReached = isZeroGroundSpeedProgressReached;
    GroundSpeed = groundSpeed;
    GroundAngleDeg = groundAngleDeg;
    GroundSide = groundSide;
    PrevGroundSide = prevGroundSide;
  }

  public static SonicViewRotatorContext FromViewContext(SonicViewContext viewContext)
  {
    return new SonicViewRotatorContext(
      viewContext.IsGrounded,
      viewContext.IsZeroGroundSpeedProgressReached,
      viewContext.GroundSpeed,
      viewContext.GroundAngleDeg,
      viewContext.GroundSide,
      viewContext.PrevGroundSide);
  }
}
