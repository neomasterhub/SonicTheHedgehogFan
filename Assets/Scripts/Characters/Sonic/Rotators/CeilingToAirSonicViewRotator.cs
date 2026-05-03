using System;
using static SonicConsts.View;

public class CeilingToAirSonicViewRotator
  : PlayerViewRotatorBase<SonicViewRotatorContext>
{
  private const float _delta = CeilingToAirAngleDegDelta;

  private float _z;

  public CeilingToAirSonicViewRotator(Func<bool> condition)
    : base("Ceiling-Air", condition)
  {
  }

  public override void Rotate(SonicViewRotatorContext context)
  {
    if (context.PrevGroundSide == GroundSide.Up)
    {
      _z = context.HorizontalDirection ? 180 : -180;
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
