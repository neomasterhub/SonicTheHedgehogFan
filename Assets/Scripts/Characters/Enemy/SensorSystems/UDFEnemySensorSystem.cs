using System;

public class UDFEnemySensorSystem : IEnemySensorSystem
{
  private Action _updateNext;
  private GroundDetectionResult? _ground;
  private WallDetectionResult? _leftWall;
  private WallDetectionResult? _rightWall;

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
