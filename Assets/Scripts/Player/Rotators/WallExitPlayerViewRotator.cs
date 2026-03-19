using System;

public class WallExitPlayerViewRotator : PlayerViewRotatorBase
{
  public WallExitPlayerViewRotator(Func<bool> condition)
    : base(condition)
  {
  }

  public override void Rotate(PlayerViewRotatorInput input)
  {
    Rotation = new(0, 0, 45);
  }
}
