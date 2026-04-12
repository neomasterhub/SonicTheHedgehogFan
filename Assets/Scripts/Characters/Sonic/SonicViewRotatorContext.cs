public readonly struct SonicViewRotatorContext
{
  public readonly float GroundAngleDeg;
  public readonly GroundSide PrevGroundSide;

  public SonicViewRotatorContext(float groundAngleDeg, GroundSide prevGroundSide)
  {
    GroundAngleDeg = groundAngleDeg;
    PrevGroundSide = prevGroundSide;
  }

  public static SonicViewRotatorContext FromViewContext(SonicViewContext viewContext)
  {
    return new SonicViewRotatorContext(
      viewContext.GroundAngleDeg,
      viewContext.PrevGroundSide);
  }
}
