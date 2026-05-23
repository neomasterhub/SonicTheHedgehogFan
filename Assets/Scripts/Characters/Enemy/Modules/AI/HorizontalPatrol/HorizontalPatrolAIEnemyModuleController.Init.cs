using static EnemyConsts.Physics;
using static SharedConsts.ConvertValues;
using static SharedConsts.Physics;

/// <summary>
/// Init.
/// </summary>
public partial class HorizontalPatrolAIEnemyModuleController
{
  public HorizontalPatrolAIEnemyModuleController()
    : base()
  {
    _speedSpx = DefaultSpeedSpx;
    _stopTimer = DefaultPatrolStopTimer;
  }

  public override void Initialize(EnemyControllerBase context)
  {
    base.Initialize(context);
    InitializePatrolArea();
    InitializeMovement();
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

  private void InitializeMovement()
  {
    _speed = _speedSpx / SpxPerUnit;
    _isStopped = false;
    _context.IsStatic = true;
  }

  private void InitializeTimers()
  {
    _stoppedTimer = new Timer(_stopTimer)
      .WhenCompleted(() => _isStopped = false);
  }
}
