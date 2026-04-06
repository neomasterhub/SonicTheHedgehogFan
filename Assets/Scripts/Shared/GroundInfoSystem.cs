using UnityEngine;
using static SharedConsts.Physics.GroundAngleRanges;

public class GroundInfoSystem
{
  private readonly Range _slopeRangeDeg = Slope;

  public GroundInfo Previous { get; private set; }
  public GroundInfo Current { get; private set; }

  public void Reset()
  {
    Previous = Current;
    Current = GroundInfo.Default;
  }

  public void Update(float sideNormalAngleDeg, float? sideNormalAngleRad = null)
  {
    Previous = Current;

    var side = Current.Side;

    if (!_slopeRangeDeg.Includes(sideNormalAngleDeg))
    {
      if (sideNormalAngleDeg < 0)
      {
        sideNormalAngleDeg += 90;
        side = side.GetPrevious();
      }
      else
      {
        sideNormalAngleDeg -= 90;
        side = side.GetNext();
      }
    }

    var angleDeg = sideNormalAngleDeg + side.GetCcwAngleDeg();
    var angleRad = angleDeg * Mathf.Deg2Rad;

    Current = new(
      sideNormalAngleDeg,
      sideNormalAngleRad ?? sideNormalAngleDeg * Mathf.Deg2Rad,
      side,
      angleDeg,
      angleRad);
  }
}
