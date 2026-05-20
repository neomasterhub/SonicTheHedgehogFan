using System;
using UnityEngine;
using static SharedConsts.Physics;

public class UDFEnemySensorSystem : IEnemySensorSystem
{
  private readonly UDFSensor _o;

  private Action _updateNext;
  private GroundDetectionResult? _ground;
  private WallDetectionResult? _leftWall;
  private WallDetectionResult? _rightWall;

  public UDFEnemySensorSystem(Vector2 parentPosition)
  {
    _o = new(Color.red, parentPosition, Vector2.up, Vector2.down, Vector2.right);
  }

  public void Update(EnemySensorContext context)
  {
    _o.SetParentPosition(context.ParentPosition);
  }

  public void Apply()
  {
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
