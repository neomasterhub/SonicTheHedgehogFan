using static EnemyConsts.Physics;
using static SharedConsts.Physics;

/// <summary>
/// Init.
/// </summary>
public partial class HorizontalPatrolAIEnemyModuleController
{
  public override void Initialize(IEnemyContext context)
  {
    base.Initialize(context);

    if (_minPositionX == 0 && _maxPositionX == 0)
    {
      var x = transform.position.x.Round(PositionRoundingDigits);
      _minPositionX = x - DefaultPatrolRadius;
      _maxPositionX = x + DefaultPatrolRadius;
    }
  }
}
