using UnityEngine;
using static SharedConsts.ConvertValues;

/// <summary>
/// Rebound.
/// </summary>
public partial class SonicController
{
  private void ApplyRebound()
  {
    if (_reboundSignal == null)
    {
      return;
    }

    var signal = _reboundSignal.Value;

    switch (signal.SourceType)
    {
      case ReboundSourceType.Block:
        ApplyRebound_Block(signal);
        break;
      default: throw signal.ArgumentOutOfRangeException();
    }
  }

  private void ApplyRebound_Block(ReboundSignal signal)
  {
    if (signal.SourceHealth < 0)
    {
      if (!_isGrounded)
      {
        if (_speedSystem.SpeedY <= 0)
        {
          _reboundAirSpeed = new(_speedSystem.SpeedX, -_speedSystem.SpeedY);
        }
        else
        {
          _reboundAirSpeed = new(_speedSystem.SpeedX, _speedSystem.SpeedY - (Mathf.Sign(_speedSystem.SpeedY) / SpxPerUnit));
        }
      }
    }
  }
}
