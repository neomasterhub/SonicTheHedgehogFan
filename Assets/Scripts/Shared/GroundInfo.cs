public readonly struct GroundInfo
{
  public static readonly GroundInfo Default = new(0, 0, GroundSide.Down, 0, 0);

  public readonly float AngleDeg;
  public readonly float AngleRad;
  public readonly GroundSide Side;
  public readonly float SideAngleDeg;
  public readonly float SideAngleRad;

  public GroundInfo(float angleDeg, float angleRad, GroundSide side, float sideAngleDeg, float sideAngleRad)
  {
    AngleDeg = angleDeg;
    AngleRad = angleRad;
    Side = side;
    SideAngleDeg = sideAngleDeg;
    SideAngleRad = sideAngleRad;
  }

  public override string ToString()
  {
    return $"{AngleDeg,3:0;-0;0} {Side.GetFirstChar()} {SideAngleDeg,3:0;-0;0}";
  }
}
