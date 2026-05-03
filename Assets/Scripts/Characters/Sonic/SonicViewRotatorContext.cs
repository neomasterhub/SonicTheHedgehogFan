public readonly struct SonicViewRotatorContext
{
  public readonly bool HorizontalDirection;
  public readonly bool IsRolling;
  public readonly float GroundAngleDeg;
  public readonly GroundSide PrevGroundSide;

  public SonicViewRotatorContext(bool horizontalDirection, bool isRolling, float groundAngleDeg, GroundSide prevGroundSide)
  {
    HorizontalDirection = horizontalDirection;
    IsRolling = isRolling;
    GroundAngleDeg = groundAngleDeg;
    PrevGroundSide = prevGroundSide;
  }

  public static SonicViewRotatorContext FromViewContext(SonicViewContext viewContext)
  {
    return new SonicViewRotatorContext(
      viewContext.HorizontalDirection,
      viewContext.IsRolling,
      viewContext.GroundAngleDeg,
      viewContext.PrevGroundSide);
  }
}
