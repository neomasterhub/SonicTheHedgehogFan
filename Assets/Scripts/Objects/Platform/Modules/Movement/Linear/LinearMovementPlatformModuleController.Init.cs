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

    _targetStopTimer = new Timer(_targetStopDuration)
      .WhenStarted(() =>
      {
        _isStopped = true;
      })
      .WhenCompleted(() =>
      {
        _isStopped = false;

        transform.position = _target;
        _target = _target == _to ? _from : _to;
      });
  }
}
