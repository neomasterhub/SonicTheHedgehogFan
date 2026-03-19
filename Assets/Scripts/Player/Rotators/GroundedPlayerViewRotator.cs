using System;
using UnityEngine;

public class GroundedPlayerViewRotator : PlayerViewRotatorBase
{
  public GroundedPlayerViewRotator(Func<bool> condition)
    : base(condition)
  {
  }

  public override void Rotate(PlayerViewRotatorInput input)
  {
    if (!Condition())
    {
      return;
    }

    Rotation = new(0, 0, Mathf.Abs(input.GroundSpeed) > input.StandingStraightGroundSpeedZone ? input.GroundAngleDeg : 0);
  }
}
