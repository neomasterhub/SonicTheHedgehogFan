using UnityEngine;
using static SharedConsts.Physics.GroundAngleRanges;

public class GroundInfoSystem
{
  private readonly Range _slopeRangeDeg = Slope;

  public float AngleDeg { get; private set; }
  public float AngleRad { get; private set; }
  public GroundSide Side { get; private set; }
  public float SideAngleDeg { get; private set; }
  public float SideAngleRad { get; private set; }

  public void Reset()
  {
    Side = GroundSide.Down;
    SideAngleDeg = 0;
    SideAngleRad = 0;
    AngleDeg = 0;
    AngleRad = 0;
  }

  public void Update(float sideNormalAngleDeg)
  {
    if (!_slopeRangeDeg.Includes(sideNormalAngleDeg))
    {
      if (sideNormalAngleDeg < 0)
      {
        sideNormalAngleDeg += 90;
        Side = Side.GetPrevious();
      }
      else
      {
        sideNormalAngleDeg -= 90;
        Side = Side.GetNext();
      }
    }

    SideAngleDeg = sideNormalAngleDeg;
    SideAngleRad = sideNormalAngleDeg * Mathf.Deg2Rad;
    AngleDeg = SideAngleDeg + Side.GetCcwAngleDeg();
    AngleRad = AngleDeg * Mathf.Deg2Rad;
  }
}
