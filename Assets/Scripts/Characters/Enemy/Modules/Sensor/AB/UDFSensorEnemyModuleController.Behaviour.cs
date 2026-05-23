using UnityEngine;
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

    DetectGround(GroundLayer);
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

  private void DetectGround(LayerMask groundLayer)
  {
    var hit = _o.DownRay.Cast(groundLayer);
    if (hit != null)
    {
      _context.Ground = new(false, hit.Value, Vector2.down, VerticalRelation.Above);
      return;
    }

    hit = _o.UpRay.Cast(groundLayer);
    if (hit != null)
    {
      _context.Ground = new(false, hit.Value, Vector2.up, VerticalRelation.Below);
      return;
    }

    _context.Ground = null;
  }
}
