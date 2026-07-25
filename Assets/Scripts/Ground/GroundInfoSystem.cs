using UnityEngine;
using static SharedConsts.Physics;

public class GroundInfoSystem
{
  public GroundInfo Previous { get; private set; }
  public GroundInfo Current { get; private set; }

  public void Reset()
  {
    Previous = Current;
    Current = GroundInfo.Default;
  }

  public void Update(float sideNormalAngleDeg)
  {
    Previous = Current;

    var side = Current.Side;

    if (!GroundAngleRanges.Steep.Includes(sideNormalAngleDeg))
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

    Current = new(
      angleDeg,
      angleDeg * Mathf.Deg2Rad,
      side,
      sideNormalAngleDeg,
      sideNormalAngleDeg * Mathf.Deg2Rad);
  }
}
