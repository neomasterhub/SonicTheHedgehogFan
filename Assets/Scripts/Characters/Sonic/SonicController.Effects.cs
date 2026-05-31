using static SonicConsts.Physics;
using static SonicConsts.View;

/// <summary>
/// Effects.
/// </summary>
public partial class SonicController
{
  private void SetEffectPipeline()
  {
    _effects.AddStep(CreateEffect_Disable());
    _effects.AddStep(CreateEffect_SetMovementFlags());
    _effects.AddStep(CreateEffect_SetHit());
    _effects.AddStep(CreateEffect_Attacked());
    _effects.AddStep(CreateEffect_GettingHit());
    _effects.AddStep(CreateEffect_LoseRings());
    _effects.AddStep(CreateEffect_Dying());
    _effects.AddStep(CreateEffect_HurtBlinking_Enter());
    _effects.AddStep(CreateEffect_Jumping_Exit());
    _effects.AddStep(CreateEffect_Jumping_Enter());
    _effects.AddStep(CreateEffect_Rolling_Exit());
    _effects.AddStep(CreateEffect_CurlingUp_Exit());
    _effects.AddStep(CreateEffect_CurlingUp_Enter());
    _effects.AddStep(CreateEffect_LookingUp_Exit());
    _effects.AddStep(CreateEffect_LookingUp_Enter());
    _effects.AddStep(CreateEffect_Static_Exit());
    _effects.AddStep(CreateEffect_StartDpadUnlockTimer());
    _effects.AddStep(CreateEffect_Rolling_Enter());
    _effects.AddStep(CreateEffect_WallDetach());
    _effects.AddStep(CreateEffect_CeilingDetach());
  }

  private PipelineStep CreateEffect_Disable()
  {
    return PipelineStepBuilder.Create()
      .WithDisplayName("Disable")
      .WithCondition(() =>
        _isDead
        && gameObject.activeSelf)
      .WithAction(() =>
      {
        gameObject.SetActive(false);

        return PipelineStepResult.Break;
      })
      .Build();
  }

  private PipelineStep CreateEffect_SetMovementFlags()
  {
    return PipelineStepBuilder.Create()
      .WithDisplayName("Set movement flags")
      .WithAction(() =>
      {
        if (_isGrounded)
        {
          _canMoveLeft = !_speedSystem.IsStoppedByLeftWall;
          _canMoveRight = !_speedSystem.IsStoppedByRightWall;
        }
        else
        {
          if (_speedSystem.IsStoppedByLeftWall)
          {
            _canMoveLeft = false;
          }

          if (_speedSystem.IsStoppedByRightWall)
          {
            _canMoveRight = false;
          }
        }

        return PipelineStepResult.Continue;
      })
      .Build();
  }

  private PipelineStep CreateEffect_SetHit()
  {
    return PipelineStepBuilder.Create()
      .WithDisplayName("Set hit")
      .WithCondition(() =>
        _takeLeftHit
        || _takeRightHit)
      .WithAction(() =>
      {
        IsHit = true;

        return PipelineStepResult.Continue;
      })
      .Build();
  }

  private PipelineStep CreateEffect_Attacked()
  {
    return PipelineStepBuilder.Create()
      .WithDisplayName("Attacked")
      .WithCondition(() =>
        !IsHit
        && ContactEnemy != null
        && !IsHurt
        && !IsInvincible
        && !IsAttacking)
      .WithAction(() =>
      {
        IsHit = true;

        return PipelineStepResult.Continue;
      })
      .Build();
  }

  private PipelineStep CreateEffect_GettingHit()
  {
    return PipelineStepBuilder.Create()
      .WithDisplayName("Getting hit")
      .WithCondition(() =>
        IsHit)
      .WithAction(() =>
      {
        IsHurt = true;
        IsInvincible = true;
        IsAttacking = false;
        CanCollectRing = false;
        AnalyzeEnvironment_Airborne();

        return PipelineStepResult.Continue;
      })
      .Build();
  }

  private PipelineStep CreateEffect_LoseRings()
  {
    return PipelineStepBuilder.Create()
      .WithDisplayName("Lose rings")
      .WithCondition(() =>
        IsHit
        && Rings.Count > 0)
      .WithAction(() =>
      {
        _ringsLost = true;
        LoseRings();

        return PipelineStepResult.Break;
      })
      .Build();
  }

  private PipelineStep CreateEffect_Dying()
  {
    return PipelineStepBuilder.Create()
      .WithDisplayName("Dying")
      .WithCondition(() =>
        IsHit
        && Rings.Count == 0)
      .WithAction(() =>
      {
        _isDying = true;
        IsHurt = false;
        _viewSystem.StopBlinking();
        _timerSystem.StartIfNotRunning(_dyingTimer);
        AnalyzeEnvironment_Airborne();

        return PipelineStepResult.Break;
      })
      .Build();
  }

  private PipelineStep CreateEffect_HurtBlinking_Enter()
  {
    return PipelineStepBuilder.Create()
      .WithDisplayName("Hurt blinking/Enter")
      .WithCondition(() =>
        IsHurt
        && !_prevIsGrounded
        && _isGrounded)
      .WithAction(() =>
      {
        IsHurt = false;
        _timerSystem.StartIfNotRunning(_postHurtInvincibleTimer);
        _timerSystem.StartIfNotRunning(_ringCollectorDisabledTimer);
        _viewSystem.StartBlinking(0, PostHurtInvincibleDuration, BlinkingInterval);

        return PipelineStepResult.Continue;
      })
      .Build();
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
        IsAttacking = true;
        SetSizes(SonicSizeMode.Small);

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
        IsAttacking = false;
        SetSizes(SonicSizeMode.Big);

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
        SetSizes(SonicSizeMode.Big);
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
        SetSizes(SonicSizeMode.Small);
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
          SetSizes(SonicSizeMode.Big);
        }

        _isCurlingUp = false;
        _isLookingUp = false;

        return PipelineStepResult.Continue;
      })
      .Build();
  }

  private PipelineStep CreateEffect_StartDpadUnlockTimer()
  {
    return PipelineStepBuilder.Create()
      .WithDisplayName("Start D-pad unlock timer")
      .WithCondition(() =>
        _isFallingOffWall
        && _isGrounded)
      .WithAction(() =>
      {
        _isFallingOffWall = false;
        _timerSystem.StartIfNotRunning(_dpadLockTimer);

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
        IsAttacking = true;
        SetSizes(SonicSizeMode.Small);

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
        _postWallDetachDpadLock = true;
        _postWallDetachPositionOffset = true;
        AnalyzeEnvironment_Airborne();

        return PipelineStepResult.Break;
      })
      .Build();
  }

  private PipelineStep CreateEffect_CeilingDetach()
  {
    return PipelineStepBuilder.Create()
      .WithDisplayName("Ceiling detach")
      .WithCondition(() =>
        _isUpGrounded
        && (_absGroundSpeed < _speedSystem.MinCeilingSpeed
        || (!_isRolling && !_inputSystem.Held.HasAny(PlayerInput.Left | PlayerInput.Right))))
      .WithAction(() =>
      {
        AnalyzeEnvironment_Airborne();

        return PipelineStepResult.Break;
      })
      .Build();
  }
}
