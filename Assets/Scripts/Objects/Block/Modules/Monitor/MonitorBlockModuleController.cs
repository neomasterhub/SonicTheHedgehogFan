using UnityEngine;

/// <summary>
/// Data.
/// </summary>
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BlockModuleControllerBase))]
[RequireComponent(typeof(ABSensorBlockModuleController))]
[RequireComponent(typeof(GroundSpeedBlockModuleController))]
[RequireComponent(typeof(NormalPositionBlockModuleController))]
public partial class MonitorBlockModuleController
  : BlockModuleControllerBase
{
  private readonly TimerSystem _timerSystem;

  private bool _otherModulesDisabled;
  private bool _movementModulesDisabled;
  private Animator _animator;

  private GameObject _screenObj;
  private Animator _screenAnimator;

  private BlockModuleControllerBase _core;
  private ABSensorBlockModuleController _sensorModule;
  private GroundSpeedBlockModuleController _speedModule;
  private NormalPositionBlockModuleController _positionModule;
  private BlockModuleControllerBase[] _otherModules;

  private Timer _screenActiveTimer;
  private Timer _sensorModuleEnabledTimer;
}
