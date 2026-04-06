using UnityEngine;

/// <summary>
/// Pipeline.
/// </summary>
public partial class SonicController
{
  private void FixedUpdate()
  {
    AnalyzeEnvironment();
    ApplyEffects();
    ApplyMovement();
    UpdateView();
    UpdatePosition();
    Output();
  }

  private void AnalyzeEnvironment()
  {
    _prevState = _state;
    _prevIsGrounded = _isGrounded;

    _timerSystem.Update(Time.deltaTime);
    _inputSystem.Update(!_postWallDetachInputLock);
    _sensorSystem.Update(_sizeMode, _groundInfoSystem.Current.Side, transform.position, TopUDFLengths, BottomUDFLengths);

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
    _state = SonicState.Grounded;
    _groundSide = _relativeGroundInfo.GetAbsoluteSide(_groundSide);
    _groundAngleDeg = _relativeGroundInfo.GetAbsoluteAngleDeg(_groundSide);
  }

  private void AnalyzeEnvironment_Airborn()
  {
    _isGrounded = false;
    _state = SonicState.Airborne;
    _groundSide = GroundSide.Down;
    _groundAngleDeg = 0;
  }

  private void ApplyEffects()
  {
  }

  private void ApplyMovement()
  {
    _speedContext = _isGrounded
      ? PlayerSpeedContext.GetGrounded(_prevIsGrounded, _relativeGroundInfo.AngleRad, _lastGroundDetectionResult.Distance)
      : PlayerSpeedContext.GetAirborne(_prevIsGrounded);
    _speedSystem.SetSpeed(_speedContext);
    _state = _state.SetFlag(SonicState.Skidding, _speedSystem.IsSkidding);
  }

  private void UpdateView()
  {
    _viewContext = new(_isGrounded, _speedSystem.IsSkidding, false, _speedSystem.SpeedX, _speedSystem.GroundSpeed, _groundAngleDeg, _prevGroundSide);
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
        * (int)_lastGroundDetectionResult.SensorGroundSide)
        - _groundedPositionUpwardOffset;
    }

    // SpeedX, SpeedY - offsets in units per frame.
    var pos = transform.position + _groundSide switch
    {
      GroundSide.Down => new Vector3(speedX, speedY),
      GroundSide.Right => new Vector3(-speedY, speedX),
      GroundSide.Up => new Vector3(-speedX, -speedY),
      GroundSide.Left => new Vector3(speedY, -speedX),
      _ => throw _groundSide.ArgumentOutOfRangeException(),
    };

    transform.position = new Vector3(pos.x.Round(2), pos.y.Round(2), transform.position.z);
  }
}
