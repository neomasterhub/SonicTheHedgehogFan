/// <summary>
/// Diagnostics.
/// </summary>
public partial class SonicController
{
  public void Output()
  {
    _info.Clear();

    _info.AddParLine("Prev State", _prevState);
    _info.AddParLine("Curr State", _state);

    _info.AppendLine();

    _info.AddParLine("Speed X", _speedSystem.SpeedX, 4);
    _info.AddParLine("Speed Y", _speedSystem.SpeedY, 4);

    InfoText.SetText(_info);
  }
}
