using UnityEngine;
using static Helpers.Sensors;
using static SharedConsts.Physics;

/// <summary>
/// Behaviour.
/// </summary>
public partial class ABSensorBlockModuleController
{
  public override void Apply()
  {
    ApplySensors();
    UpdatePushSpeed();
  }

  private void ApplySensors()
  {
    _a.SetParentPosition(transform.position);
    _b.SetParentPosition(transform.position);

    _context.LeftWall = DetectWall(_a, GroundLayer);
    _context.RightWall = DetectWall(_b, GroundLayer);
    _context.Ground = ABDetectGround(_a, _b, GroundLayer, !_spriteRenderer.flipX);
  }

  private void UpdatePushSpeed()
  {
    if (_context.LeftWall == null)
    {
      _context.PushSpeed = _maxPushSpeed;
    }
    else
    {
      var dist = _context.LeftWall.Value.Distance.Round(SpeedRoundingDigits);
      if (dist < _maxPushSpeed)
      {
        _context.PushSpeed = dist;
      }
    }
  }

  private WallDetectionResult? DetectWall(UDFSensor sensor, LayerMask groundLayer)
  {
    var hit = sensor.FrontRay.Cast(groundLayer);

    return hit == null
      ? null
      : new(hit.Value, sensor.FrontRay.Direction);
  }
}
