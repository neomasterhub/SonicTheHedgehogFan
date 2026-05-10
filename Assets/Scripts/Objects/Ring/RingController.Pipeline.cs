using UnityEngine;
using static SharedConsts.Physics;

/// <summary>
/// Pipeline.
/// </summary>
public partial class RingController : MonoBehaviour
{
  private void FixedUpdate()
  {
    if (!_initialized || _isCollected)
    {
      return;
    }

    ApplyEffects();

    if (!_gravityEnabled)
    {
      return;
    }

    AnalyzeEnvironment();
    ApplyMovement();
    UpdatePosition();
  }

  private void ApplyEffects()
  {
    _effects.Run(false);
  }

  private void AnalyzeEnvironment()
  {
    _configs.Update(_physicsMode);
    _sensorSystem.Update(transform.position);

    var ground = _sensorSystem.DetectGround(GroundLayer);

    if (ground != null)
    {
      _lastGroundDetectionResult = ground.Value;
      AnalyzeEnvironment_Grounded();
    }
    else
    {
      AnalyzeEnvironment_Airborne();
    }
  }

  private void AnalyzeEnvironment_Airborne()
  {
    _isGrounded = false;
    _speedContext = RingSpeedContext.GetAirborne();
  }

  private void AnalyzeEnvironment_Grounded()
  {
    _isGrounded = true;
    _speedContext = RingSpeedContext.GetGrounded(_lastGroundDetectionResult.Normal);
  }

  private void ApplyMovement()
  {
    _speedSystem.SetSpeed(_speedContext);
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
