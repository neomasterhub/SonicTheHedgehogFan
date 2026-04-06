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

    _info.AddParLine("Side", _groundInfoSystem.Previous.Side);
    _info.AddParLine("Side", _groundInfoSystem.Current.Side);

    _info.AppendLine();

    _info.AddParLine("Speed X", _speedSystem.SpeedX, 4);
    _info.AddParLine("Speed Y", _speedSystem.SpeedY, 4);

    InfoText.SetText(_info);
  }
}
