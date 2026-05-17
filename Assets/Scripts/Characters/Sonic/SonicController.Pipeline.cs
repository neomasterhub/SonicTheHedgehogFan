using UnityEngine;
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
    ApplyMovement();
    UpdateView();
    UpdatePosition();
    UpdateSounds();
    UpdateDebugInfo();
    EndFrame();
  }

  private void BeginFrame()
  {
    _configs.Update(_physicsMode);
    _timerSystem.Update(Time.deltaTime);

    _prevSizeMode = _sizeMode;
    _prevIsRolling = _isRolling;
    _prevIsGrounded = _isGrounded;
    _prevPhysicsMode = _physicsMode;
  }

  private void UpdateInput()
  {
    _inputSystem.Update();

    if (_inputSystem.Pressed == PlayerInput.None)
    {
      return;
    }

    if (_inputSystem.CheckLastPressed(ToggleDebugMode))
    {
      _showDebugInfo = !_showDebugInfo;
    }

    if (!IsHurt)
    {
      if (_inputSystem.CheckLastPressed(TakeLeftHit))
      {
        _takeLeftHit = true;
      }
      else if (_inputSystem.CheckLastPressed(TakeRightHit))
      {
        _takeRightHit = true;
      }
    }
  }

  private void AnalyzeEnvironment()
  {
    var groundDetectionResult = DetectGround();
    _leftWallDetectionResult = _sensorSystem.DetectLeftWall(GroundLayer);
    _rightWallDetectionResult = _sensorSystem.DetectRightWall(GroundLayer);

    if (groundDetectionResult != null)
    {
      _lastGroundDetectionResult = groundDetectionResult.Value;
      AnalyzeEnvironment_Grounded();
    }
    else
    {
      AnalyzeEnvironment_Airborne();
    }
  }

  private void AnalyzeEnvironment_Grounded()
  {
    _isGrounded = true;
    _isBalancing = _lastGroundDetectionResult.IsBalancing;
    _triggeredGroundSensorSide = _lastGroundDetectionResult.SourceSensorSide;
    _groundInfoSystem.Update(_lastGroundDetectionResult.AngleDeg);
    _slopeFactor = GetSlopeFactor(_configs.PhysicsModeConfig);

    _absGroundSpeed = Mathf.Abs(_speedSystem.GroundSpeed);
    _isDownGrounded = _groundInfoSystem.Current.Side == GroundSide.Down;
    _isDownGroundedStatic = _isDownGrounded && _speedSystem.GroundSpeed == 0;
    _isDownGroundedMoving = _isDownGrounded && _speedSystem.GroundSpeed != 0;
    _isUpGrounded = _groundInfoSystem.Current.Side == GroundSide.Up;
    _isWallGrounded = _groundInfoSystem.Current.Side is GroundSide.Left or GroundSide.Right;
  }

  private void AnalyzeEnvironment_Airborne()
  {
    _isGrounded = false;
    _isBalancing = false;
    _triggeredGroundSensorSide = false;
    _groundInfoSystem.Reset();
    _slopeFactor = 0;

    _absGroundSpeed = 0;
    _isDownGrounded = false;
    _isDownGroundedStatic = false;
    _isDownGroundedMoving = false;
    _isUpGrounded = false;
    _isWallGrounded = false;
  }

  private void ApplyEffects()
  {
    _effects.Run(_showDebugInfo);
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
        _isDownGrounded && _rightWallDetectionResult?.AngleDeg == 0 ? _rightWallDetectionResult.Value.Distance : null);
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
        _rightWallDetectionResult?.Distance);
    }

    _speedSystem.SetSpeed(_speedContext);
  }

  private void UpdateView()
  {
    _viewContext = new(!_spriteRenderer.flipX, IsHurt, _isDying, _isGrounded, _speedSystem.IsSkidding, _isBalancing, _isCurlingUp, _isLookingUp, _isRolling, _speedSystem.IsZeroGroundSpeedProgressReached, _triggeredGroundSensorSide, _speedSystem.SpeedX, _speedSystem.GroundSpeed, _groundInfoSystem.Current.AngleDeg, Time.fixedDeltaTime, _groundInfoSystem.Current.Side, _groundInfoSystem.Previous.Side);
    _viewSystem.Update(_viewContext);
  }

  private void UpdatePosition()
  {
    var speedX = _speedSystem.SpeedX;
    var speedY = _speedSystem.SpeedY;

    if (_isGrounded)
    {
      // Snap to ground with small upward offset.
      // Keeps surface normal aligned with slope.
      speedY -= (_lastGroundDetectionResult.Distance
        * (int)_lastGroundDetectionResult.SensorGroundRelation)
        - GroundedPositionUpwardOffset;
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
    var pos = transform.position + _groundInfoSystem.Current.Side switch
    {
      GroundSide.Down => new Vector3(speedX, speedY),
      GroundSide.Right => new Vector3(-speedY, speedX),
      GroundSide.Up => new Vector3(-speedX, -speedY),
      GroundSide.Left => new Vector3(speedY, -speedX),
      _ => throw _groundInfoSystem.Current.Side.ArgumentOutOfRangeException(),
    };

    transform.position = new Vector3(pos.x.Round(3), pos.y.Round(3), transform.position.z);
  }

  private void UpdateSounds()
  {
    for (var i = 0; i < _sounds.Length; i++)
    {
      _sounds[i].Stop();
      _sounds[i].Play();
    }
  }

  private void EndFrame()
  {
    _ringCollected = false;
    _ringsLost = false;
    _takeLeftHit = false;
    _takeRightHit = false;
    IsHit = false;
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

    return ContactEnemyInfo == null
      ? 0
      : Mathf.Sign(transform.position.x - ContactEnemyInfo.Value.Position.x);
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

  private GroundDetectionResult? DetectGround()
  {
    var sensorFlags = GetSensorFlags();
    _sensorSystem.Update(new(_sizeMode, _groundInfoSystem.Current.Side, transform.position, sensorFlags, _sensorRayLengths));
    var result = _sensorSystem.DetectGround(!_spriteRenderer.flipX, GroundLayer);

    if (result != null && result.Value.Distance == 0)
    {
      transform.position += new Vector3(0, GroundedPositionUpwardOffset);
      _sensorSystem.Update(new(_sizeMode, _groundInfoSystem.Current.Side, transform.position, sensorFlags, _sensorRayLengths));
      result = _sensorSystem.DetectGround(!_spriteRenderer.flipX, GroundLayer);
    }

    return result;
  }

  private void LoseRings()
  {
    var flip = _spriteRenderer.flipX;
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
