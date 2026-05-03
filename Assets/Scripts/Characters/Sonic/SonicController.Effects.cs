/// <summary>
/// Effects.
/// </summary>
public partial class SonicController
{
  private void SetEffectPipeline()
  {
    _effects.AddStep(CreateEffect_Jumping_Exit());
    _effects.AddStep(CreateEffect_Jumping_Enter());
    _effects.AddStep(CreateEffect_Rolling_Exit());
    _effects.AddStep(CreateEffect_CurlingUp_Exit());
    _effects.AddStep(CreateEffect_CurlingUp_Enter());
    _effects.AddStep(CreateEffect_LookingUp_Exit());
    _effects.AddStep(CreateEffect_LookingUp_Enter());
    _effects.AddStep(CreateEffect_Static_Exit());
    _effects.AddStep(CreateEffect_StartInputUnlockTimer());
    _effects.AddStep(CreateEffect_Rolling_Enter());
    _effects.AddStep(CreateEffect_WallDetach());
  }

  private PipelineStep CreateEffect_Jumping_Exit()
  {
    return PipelineStepBuilder.Create()
      .WithDisplayName("Jumping/Exit")
      .WithCondition(() =>
        _isJumping
        && ((_isGrounded && !_prevIsGrounded) // double jump
        || (!_isGrounded && _speedSystem.SpeedY <= 0)))
      .WithAction(() =>
      {
        _isJumping = false;

        return PipelineStepResult.Continue;
      })
      .Build();
  }

  private PipelineStep CreateEffect_Jumping_Enter()
  {
    return PipelineStepBuilder.Create()
      .WithDisplayName("Jumping/Enter")
      .WithCondition(() =>
        !_isJumping
        && _isGrounded
        && !(_isCurlingUp || _isLookingUp)
        && _inputSystem.Pressed.HasAny(PlayerInput.C))
      .WithAction(() =>
      {
        _isJumping = true;
        _isRolling = true;
        _sizeMode = SonicSizeMode.Small;

        return PipelineStepResult.Break;
      })
      .Build();
  }

  private PipelineStep CreateEffect_Rolling_Exit()
  {
    return PipelineStepBuilder.Create()
      .WithDisplayName("Rolling/Exit")
      .WithCondition(() =>
        _isDownGroundedStatic
        && _isRolling)
      .WithAction(() =>
      {
        _isRolling = false;
        _sizeMode = SonicSizeMode.Big;

        return PipelineStepResult.Continue;
      })
      .Build();
  }

  private PipelineStep CreateEffect_CurlingUp_Exit()
  {
    return PipelineStepBuilder.Create()
      .WithDisplayName("Curling up/Exit")
      .WithCondition(() =>
        _isCurlingUp
        && _inputSystem.Released == PlayerInput.Down)
      .WithAction(() =>
      {
        _sizeMode = SonicSizeMode.Big;
        _isCurlingUp = false;

        return PipelineStepResult.Break;
      })
      .Build();
  }

  private PipelineStep CreateEffect_CurlingUp_Enter()
  {
    return PipelineStepBuilder.Create()
      .WithDisplayName("Curling up/Enter")
      .WithCondition(() =>
        _isDownGroundedStatic
        && !_isBalancing
        && _inputSystem.Held.HasAny(PlayerInput.Down))
      .WithAction(() =>
      {
        _sizeMode = SonicSizeMode.Small;
        _isCurlingUp = true;

        return PipelineStepResult.Break;
      })
      .Build();
  }

  private PipelineStep CreateEffect_LookingUp_Exit()
  {
    return PipelineStepBuilder.Create()
      .WithDisplayName("Looking up/Exit")
      .WithCondition(() =>
        _isLookingUp
        && _inputSystem.Released == PlayerInput.Up)
      .WithAction(() =>
      {
        _isLookingUp = false;

        return PipelineStepResult.Break;
      })
      .Build();
  }

  private PipelineStep CreateEffect_LookingUp_Enter()
  {
    return PipelineStepBuilder.Create()
      .WithDisplayName("Looking up/Enter")
      .WithCondition(() =>
        _isDownGroundedStatic
        && !_isBalancing
        && _inputSystem.Held.HasAny(PlayerInput.Up))
      .WithAction(() =>
      {
        _isLookingUp = true;

        return PipelineStepResult.Break;
      })
      .Build();
  }

  private PipelineStep CreateEffect_Static_Exit()
  {
    return PipelineStepBuilder.Create()
      .WithDisplayName("Static/Exit")
      .WithCondition(() => _isDownGroundedMoving)
      .WithAction(() =>
      {
        if (_isCurlingUp)
        {
          _sizeMode = SonicSizeMode.Big;
        }

        _isCurlingUp = false;
        _isLookingUp = false;

        return PipelineStepResult.Continue;
      })
      .Build();
  }

  private PipelineStep CreateEffect_StartInputUnlockTimer()
  {
    return PipelineStepBuilder.Create()
      .WithDisplayName("Start input unlock timer")
      .WithCondition(() =>
        _isFallingOffWall
        && _isGrounded)
      .WithAction(() =>
      {
        _isFallingOffWall = false;
        _timerSystem.StartIfNotRunning(_inputUnlockTimer);

        return PipelineStepResult.Break;
      })
      .Build();
  }

  private PipelineStep CreateEffect_Rolling_Enter()
  {
    return PipelineStepBuilder.Create()
      .WithDisplayName("Rolling/Enter")
      .WithCondition(() =>
        _isDownGrounded
        && _absGroundSpeed >= _configs.PhysicsModeConfig.DecelerationSpeed
        && !_isBalancing
        && !_inputSystem.Held.HasAny(PlayerInput.Left | PlayerInput.Right)
        && _inputSystem.Pressed.HasAny(PlayerInput.Down))
      .WithAction(() =>
      {
        _isRolling = true;
        _sizeMode = SonicSizeMode.Small;

        return PipelineStepResult.Break;
      })
      .Build();
  }

  private PipelineStep CreateEffect_WallDetach()
  {
    return PipelineStepBuilder.Create()
      .WithDisplayName("Wall detach")
      .WithCondition(() =>
        _isWallGrounded
        && _absGroundSpeed < _speedSystem.MinWallSpeed)
      .WithAction(() =>
      {
        _isFallingOffWall = true;
        _postWallDetachInputLock = true;
        _postWallDetachPositionOffset = true;
        AnalyzeEnvironment_Airborn();

        return PipelineStepResult.Break;
      })
      .Build();
  }
}
