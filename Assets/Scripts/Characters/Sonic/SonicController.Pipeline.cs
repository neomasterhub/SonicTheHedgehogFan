using UnityEngine;
using static SharedConsts.Physics;
using static SharedConsts.SecretCodes;
using static SonicConsts.Physics;

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
    Output();
  }

  private void BeginFrame()
  {
    _prevState = _state;
    _prevIsGrounded = _isGrounded;
    _timerSystem.Update(Time.deltaTime);
  }

  private void UpdateInput()
  {
    _inputSystem.Update();

    var pressed = _inputSystem.Pressed;
    if (pressed == PlayerInput.None)
    {
      return;
    }

    if (_inputSystem.CheckLastPressed(ToggleDebugMode))
    {
      _infoPanel.SetActive(!_infoPanel.activeSelf);
    }
  }

  private void AnalyzeEnvironment()
  {
    _sensorSystem.Update(new(_sizeMode, _groundInfoSystem.Current.Side, transform.position, new(true, false, _isGrounded), new(OLength, TopUDFLengths, BottomUDFLengths)));

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
    _state = SonicState.Grounded.Set(SonicState.Balancing, _isBalancing);
  }

  private void AnalyzeEnvironment_Airborn()
  {
    _isGrounded = false;
    _isBalancing = false;
    _triggeredGroundSensorSide = false;
    _groundInfoSystem.Reset();
    _state = SonicState.Airborne.Set(SonicState.FallingOffWall, _isFallingOffWall);
  }

  private void ApplyEffects()
  {
    ApplyEffects_Grounded();
  }

  private void ApplyEffects_Grounded()
  {
    if (!_isGrounded)
    {
      return;
    }

    if (_isFallingOffWall)
    {
      _isFallingOffWall = false;
      _timerSystem.StartIfNotRunning(_inputUnlockTimer);
      return;
    }

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

  private void ApplyMovement()
  {
    _speedContext = _isGrounded
      ? PlayerSpeedContext.GetGrounded(_prevIsGrounded, _groundInfoSystem.Current.SideAngleRad, _lastGroundDetectionResult.Distance)
      : PlayerSpeedContext.GetAirborne(_prevIsGrounded);
    _speedSystem.SetSpeed(_speedContext);
    _state = _state.Set(SonicState.Skidding, _speedSystem.IsSkidding);
  }

  private void UpdateView()
  {
    _viewContext = new(_isGrounded, _speedSystem.IsSkidding, _isBalancing, _speedSystem.IsZeroGroundSpeedProgressReached, _triggeredGroundSensorSide, _speedSystem.SpeedX, _speedSystem.GroundSpeed, _groundInfoSystem.Current.AngleDeg, _groundInfoSystem.Current.Side, _groundInfoSystem.Previous.Side);
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
}
