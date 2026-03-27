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
    if (float.IsNaN(angleDeg))
    {
      return;
    }

    AngleDeg = angleDeg;
    AngleRad = angleDeg * Mathf.Deg2Rad;

    if (!_slopeRangeDeg.Has(AngleDeg))
    {
      RangeId = GroundRangeId.Steep;
      Side = AngleDeg < 0 ? GroundSide.Left : GroundSide.Right;
    }
    else if (!_flatRangeDeg.Has(AngleDeg))
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
}
