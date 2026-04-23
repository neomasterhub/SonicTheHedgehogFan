/// <summary>
/// Effects.
/// </summary>
public partial class SonicController
{
  private void ApplyEffects_Grounded_Moving_ExitStatic()
  {
    if (_isCurlingUp)
    {
      _sizeMode = SonicSizeMode.Big;
    }

    _isCurlingUp = false;
    _isLookingUp = false;
  }
}
