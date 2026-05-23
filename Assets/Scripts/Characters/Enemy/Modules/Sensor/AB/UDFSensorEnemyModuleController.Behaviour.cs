using UnityEngine;
using static Helpers.Sensors;
using static SharedConsts.Physics;

/// <summary>
/// Behaviour.
/// </summary>
public partial class ABSensorEnemyModuleController
{
  public override void Apply()
  {
    UpdateSensorSystem();

    _context.LeftWall = DetectWall(_a, GroundLayer);
    _context.RightWall = DetectWall(_b, GroundLayer);
    _context.Ground = ABDetectGround(_a, _b, GroundLayer, !_spriteRenderer.flipX);
  }

  private void UpdateSensorSystem()
  {
    _a.SetParentPosition(transform.position);
    _b.SetParentPosition(transform.position);
  }

  private WallDetectionResult? DetectWall(UDFSensor sensor, LayerMask groundLayer)
  {
    var hit = sensor.FrontRay.Cast(groundLayer);

    return hit == null
      ? null
      : new(hit.Value.distance, Vector2.SignedAngle(-sensor.FrontRay.Direction, hit.Value.normal).Round());
  }
}
