using static EnemyConsts.Physics;
using static SharedConsts.Physics;

/// <summary>
/// Init.
/// </summary>
public partial class GroundEnemyAIController
{
  public GroundEnemyAIController()
    : base(EnemyAISystemType.Ground)
  {
    SetEffectPipeline();
  }

  private void Awake()
  {
    if (_minPositionX == 0 && _maxPositionX == 0)
    {
      _minPositionX = (transform.position.x - DefaultPatrolRadius).Round(PositionRoundingDigits);
      _maxPositionX = (transform.position.x + DefaultPatrolRadius).Round(PositionRoundingDigits);
    }
  }
}
