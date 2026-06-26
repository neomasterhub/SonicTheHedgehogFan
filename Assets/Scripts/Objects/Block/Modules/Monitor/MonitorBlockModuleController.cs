using UnityEngine;

/// <summary>
/// Data.
/// </summary>
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(ABSensorBlockModuleController))]
[RequireComponent(typeof(GroundSpeedBlockModuleController))]
[RequireComponent(typeof(NormalPositionBlockModuleController))]
public partial class MonitorBlockModuleController
  : BlockModuleControllerBase
{
  private readonly TimerSystem _timerSystem;

  private Animator _animator;

  private GameObject _screenObj;
  private Animator _screenAnimator;

  private ABSensorBlockModuleController _sensorModule;
  private GroundSpeedBlockModuleController _speedModule;
  private NormalPositionBlockModuleController _positionModule;
  private BlockModuleControllerBase[] _otherModules;

  private Timer _screenActiveTimer;
}
