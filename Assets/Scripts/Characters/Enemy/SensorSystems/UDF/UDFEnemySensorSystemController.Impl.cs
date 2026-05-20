using System;
using UnityEngine;
using static SharedConsts.Physics;

/// <summary>
/// Implementation.
/// </summary>
public partial class UDFEnemySensorSystemController
{
  public EnemySensorSystemType Type => EnemySensorSystemType.UFD;

  public void UpdateSystem(EnemySensorContext context)
  {
    _o.SetParentPosition(context.ParentPosition);
    _o.FrontRay.Direction = _spriteRenderer.flipX ? Vector2.left : Vector2.right;
  }

  public void Draw()
  {
    _o.Draw();
  }

  public void Apply()
  {
    DetectWall(GroundLayer);
    DetectGround(GroundLayer);
  }

  public void SetNext(IEnemyAI next)
  {
    switch (next.Type)
    {
      case EnemyAIType.Ground:

        var ai = (GroundEnemyAIController)next;
        _updateNext = () =>
        {
          ai.SetContext(new(_ground.HasValue, _ground?.AngleRad));
        };

        break;

      default: throw new NotSupportedException();
    }
  }

  public void UpdateNext()
  {
    _updateNext();
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
