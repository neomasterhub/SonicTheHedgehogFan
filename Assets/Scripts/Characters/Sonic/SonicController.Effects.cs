/// <summary>
/// Effects.
/// </summary>
public partial class SonicController
{
  private void ApplyEffects_Grounded()
  {
    if (_speedSystem.GroundSpeed == 0)
    {
      ApplyEffects_Grounded_Static();
    }
    else
    {
      ApplyEffects_Grounded_Moving();
    }
  }

  private void ApplyEffects_Grounded_Static()
  {
  }

  private void ApplyEffects_Grounded_Moving()
  {
    ApplyEffects_Grounded_Moving_ExitStatic();
  }

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
