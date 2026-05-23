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
    DetectWall(GroundLayer);
    DetectGround(GroundLayer);
  }

  private void UpdateSensorSystem()
  {
    _o.SetParentPosition(transform.position);
    _o.FrontRay.Direction = _spriteRenderer.flipX ? Vector2.left : Vector2.right;
  }

  private void DetectWall(LayerMask groundLayer)
  {
    var hit = _o.FrontRay.Cast(groundLayer);

    _context.Wall = hit == null
      ? null
      : new(hit.Value.distance, Vector2.SignedAngle(-_o.FrontRay.Direction, hit.Value.normal).Round());
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
