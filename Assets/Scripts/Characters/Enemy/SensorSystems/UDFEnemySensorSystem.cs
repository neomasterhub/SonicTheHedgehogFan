using System;

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
}
