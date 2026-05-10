using UnityEngine;
using static RingConsts.UI;
using static SharedConsts.Physics;
using AnimatorParameters = SharedConsts.Animator.Parameters;

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

    CollectByPlayer();

    if (!_gravityEnabled)
    {
      return;
    }

    AnalyzeEnvironment();
    ApplyMovement();
    UpdatePosition();
  }

  private void CollectByPlayer()
  {
    if (_player == null)
    {
      return;
    }

    if (_collider.bounds.Intersects(_playerCollider.bounds))
    {
      _isCollected = true;
      _playerRings.Add();
      _animator.SetTrigger(AnimatorParameters.Collected);
      _spriteRenderer.sortingOrder = SparkleOrderInLayer;
    }
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
