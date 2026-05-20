using System;
using UnityEngine;
using static SharedConsts.Physics;

public class UDFEnemySensorSystem : IEnemySensorSystem
{
  private readonly UDFSensor _o;

  private Action _updateNext;
  private WallDetectionResult? _wall;
  private GroundDetectionResult? _ground;

  public UDFEnemySensorSystem(EnemySensorContext context)
  {
    _o = new(Color.red, context.ParentPosition, Vector2.up, Vector2.down, context.HorizontalDirection ? Vector2.right : Vector2.left);
  }

  public void Update(EnemySensorContext context)
  {
    _o.SetParentPosition(context.ParentPosition);
    _o.FrontRay.Direction = context.HorizontalDirection ? Vector2.right : Vector2.left;
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
