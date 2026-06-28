using UnityEngine;
using static Helpers.Math;
using static Helpers.Physics;
using static RingConsts.Physics;
using static SharedConsts.Physics;
using static SharedConsts.SecretCodes;
using static SonicConsts.Physics;
using static SonicConsts.Sizes;

/// <summary>
/// Pipeline.
/// </summary>
public partial class SonicController
{
  private void FixedUpdate()
  {
    BeginFrame();
    UpdateInput();
    AnalyzeEnvironment();
    ApplyEffects();
    ApplyRebound();
    ApplyMovement();
    UpdateView();
    UpdatePosition();
    UpdateSounds();
    UpdateDebug();
    EndFrame();
  }

  private void BeginFrame()
  {
    _configs.Update(_physicsMode);
    _timerSystem.Update(Time.deltaTime);

    _prevHasInvincibilityStars = _hasInvincibilityStars;
    _prevHasShield = _hasShield;
    _prevIsGrounded = _isGrounded;
    _prevIsRolling = _isRolling;
    _prevPhysicsMode = _physicsMode;
    _prevSizeMode = _sizeMode;

    _reboundAirSpeed = null;
    _reboundGroundSpeed = null;

    _horizontalDirection = !_spriteRenderer.flipX;
  }

  private void UpdateInput()
  {
    _inputSystem.Update();

    if (_inputSystem.Pressed == PlayerInput.None
      || _isDying)
    {
      return;
    }

    if (!IsHurt)
    {
      if (_inputSystem.CheckLastPressed(TakeLeftHit))
      {
        _takeLeftHit = true;
        return;
      }

      if (_inputSystem.CheckLastPressed(TakeRightHit))
      {
        _takeRightHit = true;
        return;
      }
    }

    if (_inputSystem.CheckLastPressed(ToggleInvincibilityStars))
    {
      _hasInvincibilityStars = !_hasInvincibilityStars;
      _invincibilityStars.SetActive(_hasInvincibilityStars);

      return;
    }

    if (_inputSystem.CheckLastPressed(ToggleShield))
    {
      _hasShield = !_hasShield;
      _shield.SetActive(_hasShield);
    }
  }

  private void AnalyzeEnvironment()
  {
    var sensorFlags = GetSensorFlags();
    _sensorSystem.Update(new(_sizeMode, _groundInfoSystem.Current.Side, transform.position, sensorFlags, _sensorRayLengths));

    _ceilingDetectionResult = DetectCeiling(sensorFlags, _horizontalDirection);
    _contactCeilingTransform = _ceilingDetectionResult?.ContactTransform;

    var ground = DetectGround(sensorFlags, _horizontalDirection);

    _leftWallDetectionResult = _sensorSystem.DetectLeftWall(SensorLayer);
    _contactLeftWallTransform = _leftWallDetectionResult?.ContactTransform;

    _rightWallDetectionResult = _sensorSystem.DetectRightWall(SensorLayer);
    _contactRightWallTransform = _rightWallDetectionResult?.ContactTransform;

    if (ground.HasValue)
    {
      _isRollingJumped = false;
      _lastGroundDetectionResult = ground.Value;

      AnalyzeEnvironment_Grounded();
    }
    else
    {
      _contactPlatform = null;

      AnalyzeEnvironment_Airborne();
    }
  }

  private void AnalyzeEnvironment_Grounded()
  {
    _isGrounded = true;
    _isBalancing = _lastGroundDetectionResult.IsBalancing;
    _triggeredGroundSensorId = _lastGroundDetectionResult.SourceSensorId;
    _groundInfoSystem.Update(_lastGroundDetectionResult.AngleDeg);
    _slopeFactor = GetSlopeFactor(_configs.PhysicsModeConfig);
    _contactGroundTransform = _lastGroundDetectionResult.ContactTransform;

    var groundSide = _groundInfoSystem.Current.Side;
    _absGroundSpeed = Mathf.Abs(_speedSystem.GroundSpeed);
    _isDownGrounded = groundSide == GroundSide.Down;
    _isDownGroundedStatic = _isDownGrounded && _speedSystem.GroundSpeed == 0;
    _isDownGroundedMoving = _isDownGrounded && _speedSystem.GroundSpeed != 0;
    _isUpGrounded = groundSide == GroundSide.Up;
    _isLeftGrounded = groundSide == GroundSide.Left;
    _isRightGrounded = groundSide == GroundSide.Right;
  }

  private void AnalyzeEnvironment_Airborne()
  {
    _isGrounded = false;
    _isBalancing = false;
    _triggeredGroundSensorId = default;
    _groundInfoSystem.Reset();
    _slopeFactor = 0;
    _contactGroundTransform = null;

    _absGroundSpeed = 0;
    _isDownGrounded = false;
    _isDownGroundedStatic = false;
    _isDownGroundedMoving = false;
    _isUpGrounded = false;
    _isLeftGrounded = false;
    _isRightGrounded = false;
  }

  private void ApplyEffects()
  {
    _effects.WithHistoryWriting(_debugMode).Run();
  }

  private void ApplyMovement()
  {
    if (_isGrounded)
    {
      _speedContext = SonicSpeedContext.GetGrounded(
        IsHit,
        GetHitHorizontalDirection(),
        _isDying,
        _isRolling,
        _isJumping,
        _prevIsGrounded,
        _groundInfoSystem.Current.SideAngleRad,
        _lastGroundDetectionResult.Distance,
        _isDownGrounded && _leftWallDetectionResult?.AngleDeg == 0 ? _leftWallDetectionResult.Value.Distance : null,
        _isDownGrounded && _rightWallDetectionResult?.AngleDeg == 0 ? _rightWallDetectionResult.Value.Distance : null,
        ContactBlock,
        _reboundGroundSpeed,
        _isStoppedByCeiling);
    }
    else
    {
      _speedContext = SonicSpeedContext.GetAirborne(
        IsHit,
        GetHitHorizontalDirection(),
        _isDying,
        _isRolling,
        _isJumping,
        _prevIsGrounded,
        _leftWallDetectionResult?.Distance,
        _rightWallDetectionResult?.Distance,
        _ceilingDetectionResult?.AngleDeg,
        _ceilingDetectionResult?.Distance,
        _reboundAirSpeed,
        _isStoppedByCeiling);
    }

    _speedSystem.SetSpeed(_speedContext);
  }

  private void UpdateView()
  {
    _viewContext = new(_horizontalDirection, IsHurt, _isDying, _isGrounded, _speedSystem.IsSkidding, _isBalancing, _isCurlingUp, _isLookingUp, _isRolling, _speedSystem.IsPushing, _speedSystem.IsZeroGroundSpeedProgressReached, _triggeredGroundSensorId, _speedSystem.SpeedX, _speedSystem.GroundSpeed, _groundInfoSystem.Current.AngleDeg, Time.fixedDeltaTime, _groundInfoSystem.Current.Side, _groundInfoSystem.Previous.Side);
    _viewSystem.Update(_viewContext);
  }

  private void UpdatePosition()
  {
    var speedX = _speedSystem.SpeedX;
    var speedY = _speedSystem.SpeedY;

    if (_isGrounded)
    {
      speedY -= GetGroundClearance(_lastGroundDetectionResult);
    }
    else
    {
      if (_postWallDetachPositionOffset)
      {
        _postWallDetachPositionOffset = false;
        speedY = WallDetachPositionOffset.y;
        speedX = _groundInfoSystem.Previous.Side == GroundSide.Left
          ? -WallDetachPositionOffset.x
          : WallDetachPositionOffset.x;
      }
    }

    if (_sizeMode != _prevSizeMode)
    {
      speedY = _sizeMode == SonicSizeMode.Small
        ? speedY - VRadiusDelta
        : speedY + VRadiusDelta;
    }

    // SpeedX, SpeedY - offsets in units per frame.
    transform.position += _groundInfoSystem.Current.Side switch
    {
      GroundSide.Down => PositionVector3(speedX, speedY),
      GroundSide.Right => PositionVector3(-speedY, speedX),
      GroundSide.Up => PositionVector3(-speedX, -speedY),
      GroundSide.Left => PositionVector3(speedY, -speedX),
      _ => throw _groundInfoSystem.Current.Side.ArgumentOutOfRangeException(),
    };

    if (_isGrounded && _contactPlatform != null)
    {
      transform.position += _contactPlatform.Displacement;
    }

    UpdatePosition_PreventBlockWallOvershoot();
  }

  private void UpdatePosition_PreventBlockWallOvershoot()
  {
    if (!_prevIsGrounded
      && _isGrounded
      && ContactBlock != null)
    {
      if (_rightWallDetectionResult.HasValue
        && _rightWallDetectionResult.Value.Distance < WallClearance)
      {
        transform.position -= PositionVector3(WallClearance - _rightWallDetectionResult.Value.Distance, 0);
      }
      else if (_leftWallDetectionResult.HasValue
        && _leftWallDetectionResult.Value.Distance < WallClearance)
      {
        transform.position += PositionVector3(WallClearance - _leftWallDetectionResult.Value.Distance, 0);
      }
    }
  }

  private void UpdateSounds()
  {
    for (var i = 0; i < _sounds.Length; i++)
    {
      _sounds[i].Stop().Play();
    }
  }

  private void EndFrame()
  {
    _isGettingInvincibilityStarsFromMonitor = false;
    _isGettingRingFromMonitor = false;
    _isGettingShieldFromMonitor = false;
    _isStoppedByCeiling = false;
    _reboundSignal = null;
    _ringCollected = false;
    _ringsLost = false;
    _takeLeftHit = false;
    _takeRightHit = false;
    IsHit = false;
    ContactBlock = null;
    ContactEnemy = null;
  }

  private void SetSizes(SonicSizeMode sizeMode)
  {
    _sizeMode = sizeMode;
    _boxCollider.size = sizeMode == SonicSizeMode.Big ? Big.BoxColliderSize : Small.BoxColliderSize;
  }

  private float GetHitHorizontalDirection()
  {
    if (_takeLeftHit)
    {
      return -1;
    }

    if (_takeRightHit)
    {
      return 1;
    }

    return ContactEnemy == null
      ? 0
      : Mathf.Sign(transform.position.x - ContactEnemy.PositionX);
  }

  private float GetSlopeFactor(SonicPhysicsModeConfig config)
  {
    var side = _groundInfoSystem.Current.Side;

    if (side == GroundSide.Up)
    {
      return 0;
    }

    if (!_isRolling)
    {
      return config.SlopeFactor;
    }

    var sideAngle = _groundInfoSystem.Current.SideAngleDeg;

    if (sideAngle == 0)
    {
      return config.RollUphillSlopeFactor;
    }

    if (side == GroundSide.Left)
    {
      return _speedSystem.GroundSpeed < 0
        ? config.RollUphillSlopeFactor
        : config.RollDownhillSlopeFactor;
    }

    if (side == GroundSide.Right)
    {
      return _speedSystem.GroundSpeed > 0
        ? config.RollUphillSlopeFactor
        : config.RollDownhillSlopeFactor;
    }

    return Mathf.Sign(_speedSystem.GroundSpeed) == Mathf.Sign(sideAngle)
      ? config.RollUphillSlopeFactor
      : config.RollDownhillSlopeFactor;
  }

  private SonicSensorFlags GetSensorFlags()
  {
    if (_isDying)
    {
      return SonicSensorFlags.None;
    }

    return new(
      _isGrounded || _speedSystem.SpeedY <= 0,
      _isGrounded || _speedSystem.SpeedY >= 0,
      _isGrounded);
  }

  private CeilingDetectionResult? DetectCeiling(SonicSensorFlags sensorFlags, bool horizontalDirection)
  {
    var result = _sensorSystem.DetectCeiling(horizontalDirection, SensorLayer);

    if (result != null && result.Value.Distance == 0)
    {
      transform.position += new Vector3(0, CeilingDetectionOffset);
      _sensorSystem.Update(new(_sizeMode, _groundInfoSystem.Current.Side, transform.position, sensorFlags, _sensorRayLengths));
      result = _sensorSystem.DetectCeiling(horizontalDirection, SensorLayer);
    }

    return result;
  }

  private GroundDetectionResult? DetectGround(SonicSensorFlags sensorFlags, bool horizontalDirection)
  {
    var result = _sensorSystem.DetectGround(horizontalDirection, SensorLayer);

    if (result != null && result.Value.Distance == 0)
    {
      transform.position += new Vector3(0, FloorDetectionOffset);
      _sensorSystem.Update(new(_sizeMode, _groundInfoSystem.Current.Side, transform.position, sensorFlags, _sensorRayLengths));
      result = _sensorSystem.DetectGround(horizontalDirection, SensorLayer);
    }

    return result;
  }

  private void LoseRings()
  {
    var flip = !_horizontalDirection;
    var speed = LostPortion1Speed;
    var angleRad = LostInitialAngleRad;

    for (var i = 1; i <= Mathf.Min(Rings.Count, MaxLostNumber); i++)
    {
      var speedX = speed * Mathf.Cos(angleRad);
      var speedY = speed * Mathf.Sin(angleRad);

      if (flip)
      {
        speedX *= -1;
        angleRad += LostAngleStepRad;
      }

      flip = !flip;

      if (i == MaxLostNumber / 2)
      {
        angleRad = LostInitialAngleRad;
        speed = LostPortion2Speed;
      }

      Instantiate(_ringPrefab, transform.position, Quaternion.identity)
        .GetComponent<RingController>()
        .Initialize(transform.gameObject, _physicsMode, LostLifetime, speedX, speedY);
    }

    Rings.Clear();
  }
}
