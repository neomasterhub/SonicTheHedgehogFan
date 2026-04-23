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
    if (_isDownGrounded)
    {
      if (_isRolling)
      {
        _isRolling = false;
        _sizeMode = SonicSizeMode.Big;
      }

      if (!_isBalancing)
      {
        // Curling up / Looking up
        if (_inputSystem.Released == PlayerInput.Down)
        {
          _sizeMode = SonicSizeMode.Big;
          _isCurlingUp = false;
          return;
        }

        if (_inputSystem.Held.HasAny(PlayerInput.Down))
        {
          _sizeMode = SonicSizeMode.Small;
          _isCurlingUp = true;
          return;
        }

        if (_inputSystem.Released == PlayerInput.Up)
        {
          _isLookingUp = false;
          return;
        }

        if (_inputSystem.Held.HasAny(PlayerInput.Up))
        {
          _isLookingUp = true;
          return;
        }
      }
    }
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
