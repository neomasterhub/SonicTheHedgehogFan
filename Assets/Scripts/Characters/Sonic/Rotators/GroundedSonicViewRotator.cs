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
    if (Mathf.Abs(context.GroundAngleDeg) <= AngleDegStandingStraightMax)
    {
      Rotation = Vector3.zero;
      return;
    }

    Rotation = new(0, 0, context.GroundAngleDeg);
  }
}
