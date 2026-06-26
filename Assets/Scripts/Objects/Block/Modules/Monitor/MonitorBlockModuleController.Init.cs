using System.Linq;
using UnityEngine;

/// <summary>
/// Init.
/// </summary>
public partial class MonitorBlockModuleController
{
  public MonitorBlockModuleController()
    : base()
  {
    _timerSystem = new();
  }

  public override void Initialize(BlockControllerBase context)
  {
    base.Initialize(context);
    InitializeComponents();
    InitializeTimers();
  }

  private void InitializeComponents()
  {
    _animator = GetComponent<Animator>();

    _core = GetComponent<BlockControllerBase>();
    _positionModule = GetComponent<NormalPositionBlockModuleController>();
    _sensorModule = GetComponent<ABSensorBlockModuleController>();
    _speedModule = GetComponent<GroundSpeedBlockModuleController>();

    _otherModules = GetComponents<BlockModuleControllerBase>()
      .Where(m => m.enabled
        && m != this
        && m != _positionModule
        && m != _sensorModule
        && m != _speedModule)
      .ToArray();

    var screen = transform.Find("Screen");
    _screenObj = screen.gameObject;
    _screenAnimator = screen.GetComponent<Animator>();
  }

  private void InitializeTimers()
  {
    _screenActiveTimer = new Timer(1)
      .WhenCompleted(() => _screenObj.SetActive(false));

    _sensorModuleEnabledTimer = new Timer(1)
      .WhenCompleted(() => _sensorModule.enabled = false);
  }
}
