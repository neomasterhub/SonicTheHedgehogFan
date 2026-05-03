using System;
using static SonicConsts.View;

public class WallToAirSonicViewRotator
  : PlayerViewRotatorBase<SonicViewRotatorContext>
{
  private const float _delta = WallToAirAngleDegDelta;

  private float _z;

  public WallToAirSonicViewRotator(Func<bool> condition)
    : base("Wall-Air", condition)
  {
  }

  public override void Rotate(SonicViewRotatorContext context)
  {
    if (context.PrevGroundSide == GroundSide.Left)
    {
      _z = -90;
    }
    else if (context.PrevGroundSide == GroundSide.Right)
    {
      _z = 90;
    }

    if (_z > 0)
    {
      _z = Math.Max(0, _z - _delta).Round();
    }
    else if (_z < 0)
    {
      _z = Math.Min(0, _z + _delta).Round();
    }

    Rotation = new(0, 0, _z);
  }
}
