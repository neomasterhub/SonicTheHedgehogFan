using UnityEngine;
using static SharedConsts.Physics.GroundAngleRanges;

public class RelativeGroundInfo
{
  private readonly Range _flatRangeDeg = Flat;
  private readonly Range _slopeRangeDeg = Slope;

  public float AngleDeg { get; private set; }
  public float AngleRad { get; private set; }
  public GroundSide Side { get; private set; }
  public GroundRangeId RangeId { get; private set; }

  public void Update(float angleDeg)
  {
    AngleDeg = angleDeg;
    AngleRad = angleDeg * Mathf.Deg2Rad;

    if (!_slopeRangeDeg.Includes(AngleDeg))
    {
      RangeId = GroundRangeId.Steep;
      Side = AngleDeg < 0 ? GroundSide.Left : GroundSide.Right;
    }
    else if (!_flatRangeDeg.Includes(AngleDeg))
    {
      RangeId = GroundRangeId.Slope;
      Side = GroundSide.Down;
    }
    else
    {
      RangeId = GroundRangeId.Flat;
      Side = GroundSide.Down;
    }
  }

  public GroundSide GetAbsoluteSide(GroundSide currentAbsoluteSide)
  {
    if (Side == GroundSide.Left)
    {
      return Side.GetPrevious();
    }
    else if (Side == GroundSide.Right)
    {
      return Side.GetNext();
    }

    return currentAbsoluteSide;
  }
}
