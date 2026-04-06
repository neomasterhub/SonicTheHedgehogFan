using System;
using UnityEngine;
using static SonicConsts.View;

public class GroundedSonicViewRotator
  : PlayerViewRotatorBase<SonicViewRotatorContext>
{
  public GroundedSonicViewRotator(Func<bool> condition)
    : base("Grounded", condition)
  {
  }

  public override void Rotate(SonicViewRotatorContext context)
  {
    var z = Mathf.Abs(context.GroundAngleDeg) <= AngleDegStandingStraightMax ? 0 : context.GroundAngleDeg;
    Rotation = new(0, 0, z);
  }
}
