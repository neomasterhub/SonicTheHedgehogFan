using UnityEngine;
using static SharedConsts.Physics;

/// <summary>
/// Behaviour.
/// </summary>
public partial class UDFEnemyModuleController
{
  public override void Apply()
  {
    DetectWall(GroundLayer);
    DetectGround(GroundLayer);
  }

  private void DetectWall(LayerMask groundLayer)
  {
    var hit = _o.FrontRay.Cast(groundLayer);

    _wall = hit == null
      ? null
      : new(hit.Value.distance, Vector2.SignedAngle(-_o.FrontRay.Direction, hit.Value.normal).Round());
  }

  private void DetectGround(LayerMask groundLayer)
  {
    var hit = _o.DownRay.Cast(groundLayer);
    if (hit != null)
    {
      _ground = new(false, hit.Value, Vector2.down, VerticalRelation.Above);
      return;
    }

    hit = _o.UpRay.Cast(groundLayer);
    if (hit != null)
    {
      _ground = new(false, hit.Value, Vector2.up, VerticalRelation.Below);
      return;
    }

    _ground = null;
  }
}
