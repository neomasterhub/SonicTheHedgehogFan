using UnityEngine;
using static Helpers.Physics;
using static SharedConsts.Physics;

/// <summary>
/// Pipeline.
/// </summary>
public partial class AnimalController
{
  private void FixedUpdate()
  {
    AnalyzeEnvironment();
    ApplyMovement();
    UpdatePosition();
    UpdateView();
  }

  private void AnalyzeEnvironment()
  {
    _sensorSystem.Update(transform.position);

    var ground = _sensorSystem.DetectGround(GroundLayer);

    _isGrounded = ground != null
      && GroundAngleRanges.Slope.Includes(ground.Value.AngleDeg);

    if (_isGrounded)
    {
      _isRunning = true;
      _lastGroundDetectionResult = ground.Value;
    }
  }

  private void ApplyMovement()
  {
    _speedSystem.SetSpeed(new(_isGrounded));
  }

  private void UpdatePosition()
  {
    var speedY = _speedSystem.SpeedY;

    if (_isGrounded)
    {
      speedY -= GetGroundClearance(_lastGroundDetectionResult);
    }

    transform.position += new Vector3(_speedSystem.SpeedX, speedY);
  }

  private void UpdateView()
  {
    _viewSystem.Update(new(_isRunning, _speedSystem.SpeedY));
  }
}
