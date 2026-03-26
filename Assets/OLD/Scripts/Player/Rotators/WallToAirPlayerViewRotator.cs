using System;

public class WallToAirPlayerViewRotator : PlayerViewRotatorBase
{
  private float _z;

  public WallToAirPlayerViewRotator(float delta, Func<bool> condition)
    : base("Wall-to-Air", condition)
  {
    Delta = delta;
  }

  public float Delta { get; set; }

  public override void Rotate(PlayerViewRotatorInput input)
  {
    if (input.PrevPlayerState.HasFlag(PlayerState.Grounded))
    {
      if (input.PrevGroundSide == GroundSide.Left)
      {
        _z = -90;
      }
      else if (input.PrevGroundSide == GroundSide.Right)
      {
        _z = 90;
      }
      else
      {
        throw input.PrevPlayerState.ArgumentOutOfRangeException();
      }
    }

    if (_z > 0)
    {
      _z = Math.Max(0, _z - Delta).Round();
    }
    else if (_z < 0)
    {
      _z = Math.Min(0, _z + Delta).Round();
    }

    Rotation = new(0, 0, _z);
  }
}
