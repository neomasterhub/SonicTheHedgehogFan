using static Helpers.Math;
using static SonicConsts;
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
    _effects.AddStep(CreateEffect_DeathZone());
    _effects.AddStep(CreateEffect_FallingBlockOnHead());
    _effects.AddStep(CreateEffect_SetHit());
    _effects.AddStep(CreateEffect_Attacked());
    _effects.AddStep(CreateEffect_GettingHit());
    _effects.AddStep(CreateEffect_LoseShield());
    _effects.AddStep(CreateEffect_LoseRings());
    _effects.AddStep(CreateEffect_Dying());
    _effects.AddStep(CreateEffect_HurtBlinking_Enter());
    _effects.AddStep(CreateEffect_Jumping_Exit());
    _effects.AddStep(CreateEffect_Jumping_Enter());
    _effects.AddStep(CreateEffect_Rolling_Exit());
    _effects.AddStep(CreateEffect_SpinDashCharging_Exit());
    _effects.AddStep(CreateEffect_SpinDashRelease());
    _effects.AddStep(CreateEffect_SpinDashCharging_Enter());
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

  private PipelineStep CreateEffect_DeathZone()
  {
    return PipelineStepBuilder.Create()
      .WithDisplayName("Death zone")
      .WithCondition(() =>
        !_isDying
        && IntersectingZones.HasAny(ZoneType.Death))
      .WithAction(() =>
      {
        IsHit = true;
        Dying();

        return PipelineStepResult.Break;
      })
      .Build();
  }

  private PipelineStep CreateEffect_FallingBlockOnHead()
  {
    return PipelineStepBuilder.Create()
      .WithDisplayName("Falling block on head")
      .WithCondition(() =>
        !_isRolling
        && _ceilingDetectionResult.HasValue
        && ContactBlock != null
        && ContactBlock.SpeedY < _speedSystem.SpeedY)
      .WithAction(() =>
      {
        var blockX = _contactCeilingTransform.transform.position.x;
        var offsetX = Sizes.Big.HRadius + ContactBlock.HRadius + WallClearance;
        var offsetXDir = blockX < transform.position.x;
        var x = offsetXDir ? blockX + offsetX : blockX - offsetX;

        if (offsetXDir)
        {
          if (_speedSystem.SpeedX < 0)
          {
            x += _speedSystem.SpeedX;
          }
        }
        else
        {
          if (_speedSystem.SpeedX > 0)
          {
            x -= _speedSystem.SpeedX;
          }
        }

        transform.position = PositionVector3(x, transform.position.y);

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
        SpinSmoke = null;
        AnalyzeEnvironment_Airborne();

        return PipelineStepResult.Continue;
      })
      .Build();
  }

  private PipelineStep CreateEffect_LoseShield()
  {
    return PipelineStepBuilder.Create()
      .WithDisplayName("Lose shield")
      .WithCondition(() =>
        IsHit
        && _hasShield)
      .WithAction(() =>
      {
        _hasShield = false;
        _shield.SetActive(false);

        return PipelineStepResult.Break;
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
        Dying();

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
        _isRollingJumped = _isRolling;
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
        _isRolling
        && (_isDownGroundedStatic
        || (_isDownGrounded && !_prevIsGrounded)))
      .WithAction(() =>
      {
        _isRolling = false;

        if (!_hasInvincibilityStars)
        {
          IsAttacking = false;
        }

        SetSizes(SonicSizeMode.Big);

        return PipelineStepResult.Continue;
      })
      .Build();
  }

  private PipelineStep CreateEffect_SpinDashCharging_Exit()
  {
    return PipelineStepBuilder.Create()
      .WithDisplayName("Spin dash charging/Exit")
      .WithCondition(() =>
        _isSpinDashCharging
        && (!_isCurlingUp
        || !_inputSystem.Held.HasAny(PlayerInput.C)))
      .WithAction(() =>
      {
        _isSpinDashCharging = false;
        IsAttacking = false;
        SpinSmoke = null;

        return PipelineStepResult.Continue;
      })
      .Build();
  }

  private PipelineStep CreateEffect_SpinDashRelease()
  {
    return PipelineStepBuilder.Create()
      .WithDisplayName("Spin dash release")
      .WithCondition(() =>
        !_isSpinDashCharging
        && _prevIsSpinDashCharging
        && _inputSystem.Held.HasAny(PlayerInput.Down)
        && _spinDashCharge >= MinSpinDashCharge)
      .WithAction(() =>
      {
        _isSpinDashReleased = true;
        _isRolling = true;
        IsAttacking = true;
        SetSizes(SonicSizeMode.Small);

        return PipelineStepResult.Break;
      })
      .Build();
  }

  private PipelineStep CreateEffect_SpinDashCharging_Enter()
  {
    return PipelineStepBuilder.Create()
      .WithDisplayName("Spin dash charging/Enter")
      .WithCondition(() =>
        !_isSpinDashCharging
        && _isCurlingUp
        && _inputSystem.CheckLastPressed(new[] { PlayerInput.C })
        && _inputSystem.Held.HasAny(PlayerInput.C))
      .WithAction(() =>
      {
        _isSpinDashCharging = true;
        IsAttacking = true;

        SpinSmoke = Instantiate(_spinSmokePrefab, transform.position, default)
          .GetComponent<ISpinSmoke>();

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
        && (_inputSystem.Released.HasAny(PlayerInput.Down)
        || IsHurt
        || _isDying))
      .WithAction(() =>
      {
        if (!_isRolling)
        {
          SetSizes(SonicSizeMode.Big);
        }

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
        && (_inputSystem.Released.HasAny(PlayerInput.Up)
        || IsHurt
        || _isDying))
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
        if (_isCurlingUp && !_isRolling)
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
        ((_isLeftGrounded && !_horizontalDirection)
        || (_isRightGrounded && _horizontalDirection))
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

  private void Dying()
  {
    _isDying = true;
    _isRolling = false;

    IsHurt = false;

    _viewSystem.StopBlinking();

    _timerSystem.Remove(_postHurtInvincibleTimer);
    _timerSystem.Remove(_invincibilityStarsTimer);
    _timerSystem.StartIfNotRunning(_dyingTimer);

    EnableInvincibilityStars(false);

    AnalyzeEnvironment_Airborne();
  }
}
