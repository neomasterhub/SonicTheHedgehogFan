using System;
using UnityEngine;
using static Helpers.Math;
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
    Rotation = Vector3(z:
      Mathf.Abs(context.GroundAngleDeg) <= StandingStraightAngleDegMax
      ? 0 : context.GroundAngleDeg);
  }
}
