using System;

public class WallToAirSonicViewRotator
  : PlayerViewRotatorBase<SonicViewRotatorContext>
{
  private float _z;

  public WallToAirSonicViewRotator(float delta, Func<bool> condition)
    : base("Wall-to-Air", condition)
  {
    Delta = delta;
  }

  public float Delta { get; set; }

  public override void Rotate(SonicViewRotatorContext context)
  {
    if (context.PrevGroundSide == GroundSide.Left)
    {
      _z = -90;
    }
    else if (context.PrevGroundSide == GroundSide.Right)
    {
      _z = 90;
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
