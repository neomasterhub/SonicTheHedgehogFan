using System;

public class WallExitPlayerViewRotator : PlayerViewRotatorBase
{
  private const float _deltaAbs = 5;

  private float _delta;
  private float _z;

  public WallExitPlayerViewRotator(Func<bool> condition)
    : base(condition)
  {
  }

  public override void Rotate(PlayerViewRotatorInput input)
  {
    if (input.PrevPlayerState.HasFlag(PlayerState.Grounded))
    {
      if (input.PrevGroundSide == GroundSide.Left)
      {
        _z = -90;
        _delta = -_deltaAbs;
      }
      else if (input.PrevGroundSide == GroundSide.Right)
      {
        _z = 90;
        _delta = _deltaAbs;
      }
    }

    if (_z != 0)
    {
      _z -= _delta;
    }

    Rotation = new(0, 0, _z);
  }
}
