using UnityEngine;

/// <summary>
/// Behaviour.
/// </summary>
public partial class LinearMovementPlatformModuleController
{
  public override void Apply()
  {
    _timerSystem.Update(Time.fixedDeltaTime);

    if (!_isStopped)
    {
      transform.position = Vector3.MoveTowards(transform.position, _target, _speed);
    }

    if (transform.position == _target)
    {
      _timerSystem.StartIfNotRunning(_targetStopTimer);
    }
  }
}
