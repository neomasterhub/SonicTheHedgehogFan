using UnityEngine;
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
  }

  private void BeginFrame()
  {
    _timerSystem.Update(Time.deltaTime);

    _prevState = _state;
    _prevSizeMode = _sizeMode;
    _prevIsRolling = _isRolling;
    _prevIsGrounded = _isGrounded;
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
  }

  private void AnalyzeEnvironment()
  {
    _sensorSystem.Update(new(_sizeMode, _groundInfoSystem.Current.Side, transform.position, GetSensorFlags(), _sensorRayLengths));

    _leftWallDetectionResult = _sensorSystem.DetectLeftWall(_groundLayer);
    _rightWallDetectionResult = _sensorSystem.DetectRightWall(_groundLayer);

    var groundDetectionResult = _sensorSystem.DetectGround(!_spriteRenderer.flipX, _groundLayer);
    if (groundDetectionResult != null)
    {
      _lastGroundDetectionResult = groundDetectionResult.Value;
      AnalyzeEnvironment_Grounded();
    }
    else
    {
      AnalyzeEnvironment_Airborn();
    }
  }

  private void AnalyzeEnvironment_Grounded()
  {
    _isGrounded = true;
    _isBalancing = _lastGroundDetectionResult.IsBalancing;
    _triggeredGroundSensorSide = _lastGroundDetectionResult.SourceSensorSide;
    _groundInfoSystem.Update(_lastGroundDetectionResult.AngleDeg);
    _slopeFactor = GetSlopeFactor();

    _absGroundSpeed = Mathf.Abs(_speedSystem.GroundSpeed);
    _isDownGrounded = _groundInfoSystem.Current.Side == GroundSide.Down;
    _isDownGroundedStatic = _isDownGrounded && _speedSystem.GroundSpeed == 0;
    _isDownGroundedMoving = _isDownGrounded && _speedSystem.GroundSpeed != 0;

    _state = SonicState.Grounded
      .Set(SonicState.Balancing, _isBalancing)
      .Set(SonicState.CurlingUp, _isCurlingUp)
      .Set(SonicState.LookingUp, _isLookingUp)
      .Set(SonicState.Rolling, _isRolling)
      .Set(SonicState.FallingOffWall, _isFallingOffWall);
  }

  private void AnalyzeEnvironment_Airborn()
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

    _state = SonicState.Airborne
      .Set(SonicState.Rolling, _isRolling)
      .Set(SonicState.FallingOffWall, _isFallingOffWall);
  }

  private void ApplyEffects()
  {
    _effects.Run(_showDebugInfo);
  }

  private void ApplyMovement()
  {
    if (_isGrounded)
    {
      var isDownGrounded = _groundInfoSystem.Current.Side == GroundSide.Down;
      _speedContext = SonicSpeedContext.GetGrounded(
        _isRolling,
        _prevIsGrounded,
        _groundInfoSystem.Current.SideAngleRad,
        _lastGroundDetectionResult.Distance,
        isDownGrounded && _leftWallDetectionResult?.AngleDeg == 0 ? _leftWallDetectionResult.Value.Distance : null,
        isDownGrounded && _rightWallDetectionResult?.AngleDeg == 0 ? _rightWallDetectionResult.Value.Distance : null);
    }
    else
    {
      _speedContext = SonicSpeedContext.GetAirborne(
        _isRolling,
        _prevIsGrounded,
        _leftWallDetectionResult?.Distance,
        _rightWallDetectionResult?.Distance);
    }

    _speedSystem.SetSpeed(_speedContext);
    _state = _state.Set(SonicState.Skidding, _speedSystem.IsSkidding);
  }

  private void UpdateView()
  {
    _viewContext = new(_isGrounded, _speedSystem.IsSkidding, _isBalancing, _isCurlingUp, _isLookingUp, _isRolling, _speedSystem.IsZeroGroundSpeedProgressReached, _triggeredGroundSensorSide, _speedSystem.SpeedX, _speedSystem.GroundSpeed, _groundInfoSystem.Current.AngleDeg, _groundInfoSystem.Current.Side, _groundInfoSystem.Previous.Side);
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
        speedX = WallDetachPositionOffset.x;
        speedY = WallDetachPositionOffset.y;
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

    transform.position = new Vector3(pos.x.Round(2), pos.y.Round(2), transform.position.z);
  }

  private void UpdateSounds()
  {
    for (var i = 0; i < _sounds.Length; i++)
    {
      _sounds[i].Stop();
      _sounds[i].Play();
    }
  }

  private float GetSlopeFactor()
  {
    if (!_isRolling)
    {
      return SlopeFactor;
    }

    if (Mathf.Sign(_speedSystem.GroundSpeed) == Mathf.Sign(_groundInfoSystem.Current.SideAngleDeg))
    {
      return RollUphillSlopeFactor;
    }
    else
    {
      return RollDownhillSlopeFactor;
    }
  }

  private SonicSensorFlags GetSensorFlags()
  {
    return new(true, false, _isGrounded && !_isCurlingUp);
  }
}
