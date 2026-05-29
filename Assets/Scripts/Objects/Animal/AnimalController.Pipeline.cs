using UnityEngine;
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
  }

  private void AnalyzeEnvironment()
  {
    _sensorSystem.Update(transform.position);

    var ground = _sensorSystem.DetectGround(GroundLayer);

    _isGrounded = ground != null;
    if (_isGrounded)
    {
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
      speedY -= (_lastGroundDetectionResult.Distance
        * (int)_lastGroundDetectionResult.SensorGroundRelation)
        - GroundedPositionUpwardOffset;
    }

    transform.position += new Vector3(_speedSystem.SpeedX, speedY);
  }
}
