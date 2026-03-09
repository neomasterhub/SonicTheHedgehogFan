using System;
using System.Collections.Generic;

public class SlopeFactorSpeedProvider
{
  private readonly Dictionary<GroundSide, Func<float, float, float>> _factorAngleSpeed = new();

  public SlopeFactorSpeedProvider(Dictionary<GroundSide, Func<float, float, float>> factorAngleSpeed)
  {
    _factorAngleSpeed = factorAngleSpeed;
  }

  public Func<float, float, float> this[GroundSide groundSide]
  {
    get => _factorAngleSpeed[groundSide];
    set => _factorAngleSpeed[groundSide] = value;
  }
}
