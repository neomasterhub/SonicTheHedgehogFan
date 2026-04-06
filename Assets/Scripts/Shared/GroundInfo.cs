public class GroundInfo
{
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
