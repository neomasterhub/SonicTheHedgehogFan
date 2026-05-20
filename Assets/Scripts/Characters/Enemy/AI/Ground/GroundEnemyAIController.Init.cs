using static EnemyConsts.Physics;

/// <summary>
/// Init.
/// </summary>
public partial class GroundEnemyAIController
{
  public GroundEnemyAIController()
    : base(EnemyAIType.Ground)
  {
    SetEffectPipeline();
  }

  private void Awake()
  {
    if (_minPositionX == 0 && _maxPositionX == 0)
    {
      _minPositionX = transform.position.x - DefaultPatrolRadius;
      _maxPositionX = transform.position.x + DefaultPatrolRadius;
    }
  }
}
