using static EnemyConsts.Physics;
using static SharedConsts.Physics;

/// <summary>
/// Init.
/// </summary>
public partial class HorizontalPatrolAIEnemyModuleController
{
  public HorizontalPatrolAIEnemyModuleController()
    : base()
  {
    _timerSystem = new();
  }

  public override void Initialize(EnemyControllerBase context)
  {
    base.Initialize(context);
    InitializePatrolArea();
    InitializeTimers();
  }

  private void InitializePatrolArea()
  {
    if (_minPositionX == 0 && _maxPositionX == 0)
    {
      var x = transform.position.x.Round(PositionRoundingDigits);
      _minPositionX = x - DefaultPatrolRadius;
      _maxPositionX = x + DefaultPatrolRadius;
    }
  }

  private void InitializeTimers()
  {
    _stopTimer = new Timer(_stopDuration)
      .WhenCompleted(() => _isStopped = false);
  }
}
