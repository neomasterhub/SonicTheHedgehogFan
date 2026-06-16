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
    _context.LeftPushSpeed = GetSidePushSpeed(_context.LeftWall, _maxLeftPushSpeed);
    _context.RightPushSpeed = GetSidePushSpeed(_context.RightWall, _maxRightPushSpeed);
  }

  private WallDetectionResult? DetectWall(UDFSensor sensor, LayerMask groundLayer)
  {
    var hit = sensor.FrontRay.Cast(groundLayer);

    return hit == null
      ? null
      : new(hit.Value, sensor.FrontRay.Direction);
  }

  private float GetSidePushSpeed(WallDetectionResult? wall, float maxSpeed)
  {
    return wall == null
      || (wall.Value.Distance - _wallClearance).Round(SpeedRoundingDigits) > maxSpeed
      ? maxSpeed
      : 0;
  }
}
