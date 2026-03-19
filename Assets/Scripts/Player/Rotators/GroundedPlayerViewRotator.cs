using UnityEngine;

public class GroundedPlayerViewRotator : PlayerViewRotatorBase
{
  public override void Rotate(PlayerViewRotatorInput input)
  {
    if (!Enabled)
    {
      return;
    }

    Angle = Mathf.Abs(input.GroundSpeed) > input.StandingStraightGroundSpeedZone
      ? input.GroundAngleDeg
      : 0;
  }
}
