using UnityEngine;

/// <summary>
/// Pipeline.
/// </summary>
public partial class SonicController
{
  private void FixedUpdate()
  {
    AnalyzeEnvironment();
    UpdatePosition();
    Output();
  }

  private void AnalyzeEnvironment()
  {
    _prevState = _state;
    _prevGroundSide = _groundSide;
    _prevIsGrounded = _isGrounded;

    _timerSystem.Update(Time.deltaTime);
    _inputSystem.Update(!_postWallDetachInputLock);
    _sensorSystem.Update(_sizeMode, _groundSide, transform.position, TopUDFLengths, BottomUDFLengths);

    var groundDetectionResult = _sensorSystem.DetectGround(!_spriteRenderer.flipX, _groundLayer);
    if (groundDetectionResult != null)
    {
      _lastGroundDetectionResult = groundDetectionResult.Value;
      _relativeGroundInfo.Update(_lastGroundDetectionResult.AngleDeg);

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
  }

  private void AnalyzeEnvironment_Airborn()
  {
    _isGrounded = false;
    _state = SonicState.Airborne;
    _groundSide = GroundSide.Down;
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
