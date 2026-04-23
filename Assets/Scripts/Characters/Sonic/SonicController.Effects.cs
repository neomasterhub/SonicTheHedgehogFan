using UnityEngine;
using static SonicConsts.Physics;

/// <summary>
/// Effects.
/// </summary>
public partial class SonicController
{
  private void ApplyEffects()
  {
    if (_isGrounded)
    {
      ApplyEffects_Grounded();
    }
  }

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

    if (_isDownGrounded)
    {
      // Rolling
      if (!_isBalancing
        && _inputSystem.Pressed.HasAny(PlayerInput.Down))
      {
        _isRolling = true;
        _sizeMode = SonicSizeMode.Small;
        return;
      }
    }

    // Start input unlock timer
    if (_isFallingOffWall)
    {
      _isFallingOffWall = false;
      _timerSystem.StartIfNotRunning(_inputUnlockTimer);
      return;
    }

    // Wall detach
    if (_groundInfoSystem.Current.Side is GroundSide.Left or GroundSide.Right
      && Mathf.Abs(_speedSystem.GroundSpeed) < DecelerationSpeed)
    {
      _isFallingOffWall = true;
      _postWallDetachInputLock = true;
      _postWallDetachPositionOffset = true;
      AnalyzeEnvironment_Airborn();
      return;
    }
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
