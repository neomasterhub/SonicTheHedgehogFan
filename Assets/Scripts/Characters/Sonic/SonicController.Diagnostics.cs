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

    _info.AppendLine($"GP {_groundInfoSystem.Previous}");
    _info.AppendLine($"GC {_groundInfoSystem.Current}");

    InfoText.SetText(_info);
  }
}
