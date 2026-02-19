using UnityEngine;

public class GroundInfo
{
  private readonly Range _flatRangeDeg = new(-23, 23);
  private readonly Range _slopeRangeDeg = new(-45, 45);

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
    }
    else if (!_flatRangeDeg.Has(AngleDeg))
    {
      RangeId = GroundRangeId.Slope;
    }
    else
    {
      RangeId = GroundRangeId.Flat;
    }

    Debug.Log(RangeId);
  }
}
