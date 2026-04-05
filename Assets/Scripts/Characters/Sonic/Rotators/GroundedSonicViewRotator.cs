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
    Rotation = new(0, 0, Mathf.Abs(context.GroundSpeed) > SpeedStandingStraightMax ? context.GroundAngleDeg : 0);
  }
}
