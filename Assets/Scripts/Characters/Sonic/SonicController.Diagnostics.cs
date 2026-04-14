/// <summary>
/// Diagnostics.
/// </summary>
public partial class SonicController
{
  private void OnDrawGizmos()
  {
    _sensorSystem.CurrentSensorGroup.Draw();
  }

  public void Output()
  {
    _info.Clear();

    _info.AppendLine($"INH {_inputSystem.GetPressedHistory()}");
    _info.AppendLine($"GP {_groundInfoSystem.Previous}");
    _info.AppendLine($"GC {_groundInfoSystem.Current}");
    _info.AppendLine($"GS {_speedSystem.SlopeFactorSpeed * 10000,5:0;-0;0} {_speedSystem.GroundSpeed * 10000,5:0;-0;0}");
    _info.AppendLine($"SP {_speedSystem.SpeedX * 10000,5:0;-0;0} {_speedSystem.SpeedY * 10000,5:0;-0;0}");

    _infoText.SetText(_info);
  }
}
