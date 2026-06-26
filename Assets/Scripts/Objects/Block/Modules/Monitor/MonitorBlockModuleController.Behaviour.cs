using UnityEngine;
using AnimatorParameters = SharedConsts.Animator.Parameters;

/// <summary>
/// Behaviour.
/// </summary>
public partial class MonitorBlockModuleController
{
  public override void Apply()
  {
    if (!_context.IsDestroyed)
    {
      return;
    }

    _timerSystem.Update(Time.fixedDeltaTime);

    if (_movementModulesDisabled
      && _sensorModuleEnabledTimer.IsCompleted)
    {
      enabled = false;
      _core.enabled = false;

      return;
    }

    if (!_otherModulesDisabled)
    {
      DisableOtherModules();

      _timerSystem.StartIfNotRunning(_screenActiveTimer);
      _animator.SetBool(AnimatorParameters.Destroyed, true);
      _screenAnimator.SetBool(AnimatorParameters.Destroyed, true);
    }

    if (!_movementModulesDisabled
      && _context.Ground.HasValue
      && _context.SpeedY == 0)
    {
      DisableMovementModules();
    }
  }

  private void DisableOtherModules()
  {
    for (var i = 0; i < _otherModules.Length; i++)
    {
      _otherModules[i].enabled = false;
    }

    _otherModulesDisabled = true;
  }

  private void DisableMovementModules()
  {
    _speedModule.enabled = false;
    _positionModule.enabled = false;
    _timerSystem.StartIfNotRunning(_sensorModuleEnabledTimer);
    _movementModulesDisabled = true;
  }
}
