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

    _info.AddParLine("Prev State", _prevState);
    _info.AddParLine("Curr State", _state);

    _info.AppendLine();

    _info.AddParLine("Ground Side", _groundInfoSystem.Current.Side);
    _info.AddParLine("Ground Rel Angle", _groundInfoSystem.Current.SideAngleDeg, " °");
    _info.AddParLine("Ground Abs Angle", _groundInfoSystem.Current.AngleDeg, " °");
    _info.AddParLine("Slope Speed", _speedSystem.SlopeFactorSpeed, 4);
    _info.AddParLine("Ground Speed", _speedSystem.GroundSpeed, 4);

    _info.AppendLine();

    _info.AddParLine("Speed X", _speedSystem.SpeedX, 4);
    _info.AddParLine("Speed Y", _speedSystem.SpeedY, 4);

    InfoText.SetText(_info);
  }
}
