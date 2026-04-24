public readonly struct SonicViewRotatorContext
{
  public readonly bool IsRolling;
  public readonly float GroundAngleDeg;
  public readonly GroundSide PrevGroundSide;

  public SonicViewRotatorContext(bool isRolling, float groundAngleDeg, GroundSide prevGroundSide)
  {
    IsRolling = isRolling;
    GroundAngleDeg = groundAngleDeg;
    PrevGroundSide = prevGroundSide;
  }

  public static SonicViewRotatorContext FromViewContext(SonicViewContext viewContext)
  {
    return new SonicViewRotatorContext(
      viewContext.IsRolling,
      viewContext.GroundAngleDeg,
      viewContext.PrevGroundSide);
  }
}
