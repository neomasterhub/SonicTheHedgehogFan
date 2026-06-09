using static PlatformConsts;
using static SharedConsts.ConvertValues;

/// <summary>
/// Init.
/// </summary>
public partial class LinearMovementPlatformModuleController
{
  public LinearMovementPlatformModuleController()
    : base()
  {
    _timerSystem = new();

    _speedPx = SpeedPx;
    _targetStopDuration = StopDuration;
  }

  public override void Initialize(PlatformControllerBase context)
  {
    base.Initialize(context);

    _target = _to;
    _speed = _speedPx / PxPerUnit;

    var dir = (_to - _from).normalized;

    _targetStopTimer = new Timer(_targetStopDuration)
      .WhenStarted(() =>
      {
        _isStopped = true;

        _context.SpeedX = 0;
        _context.SpeedY = 0;
      })
      .WhenCompleted(() =>
      {
        _isStopped = false;

        dir = -dir;
        _context.SpeedX = _speed * dir.x;
        _context.SpeedY = _speed * dir.y;

        transform.position = _target;
        _target = _target == _to ? _from : _to;
      });
  }
}
