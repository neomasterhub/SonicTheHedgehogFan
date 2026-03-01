using UnityEngine;

public class RelativeGroundInfo
{
  private readonly Range _flatRangeDeg = Consts.Physics.GroundAngleRanges.Flat;
  private readonly Range _slopeRangeDeg = Consts.Physics.GroundAngleRanges.Slope;

  public float AngleDeg { get; private set; }
  public float AngleRad { get; private set; }
  public GroundSide Side { get; private set; }
  public GroundRangeId RangeId { get; private set; }

  public void Update(float angleDeg)
  {
    AngleDeg = angleDeg;
    AngleRad = AngleDeg * Mathf.Deg2Rad;

    if (!_slopeRangeDeg.Has(AngleDeg))
    {
      RangeId = GroundRangeId.Steep;
      Side = AngleDeg > 0 ? GroundSide.Right : GroundSide.Left;
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
