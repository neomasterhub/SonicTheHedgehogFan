using static SharedConsts.Physics.GroundAngleRanges;

public class GroundInfo
{
  private readonly Range _flatRangeDeg = Flat;
  private readonly Range _slopeRangeDeg = Slope;

  public float AngleDeg { get; private set; }
  public float AngleRad { get; private set; }
  public GroundSide Side { get; private set; }
  public GroundRangeId SideRangeId { get; private set; }
  public float SideAngleDeg { get; private set; }
  public float SideAngleRad { get; private set; }

  public void Update(float sideNormalAngleDeg)
  {
  }
}
