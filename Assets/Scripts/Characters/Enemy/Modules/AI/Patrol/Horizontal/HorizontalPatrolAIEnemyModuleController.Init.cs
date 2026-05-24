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
    _speedSpx = SpeedSpx;
    _stoppedDuration = PatrolStoppedTimer;
  }

  public override void Initialize(EnemyControllerBase context)
  {
    base.Initialize(context);
    InitializePatrolArea();
    InitializeMovement();
    InitializeContext();
    InitializeTimers();
  }

  private void InitializePatrolArea()
  {
    if (_minPositionX == 0 && _maxPositionX == 0)
    {
      var x = transform.position.x.Round(PositionRoundingDigits);
      _minPositionX = x - PatrolRadius;
      _maxPositionX = x + PatrolRadius;
    }
  }

  private void InitializeMovement()
  {
    _speed = _speedSpx / SpxPerUnit;
    _isStopped = false;
  }

  private void InitializeContext()
  {
    _context.HorizontalDirection = _speed > 0;
  }

  private void InitializeTimers()
  {
    _stoppedTimer = new Timer(_stoppedDuration)
      .WhenCompleted(() => _isStopped = false);
  }
}
