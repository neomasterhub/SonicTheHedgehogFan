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
        break;
      default: throw signal.ArgumentOutOfRangeException();
    }
  }
}
