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
  }
}
