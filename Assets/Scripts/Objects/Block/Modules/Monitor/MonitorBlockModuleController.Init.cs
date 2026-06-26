using System.Linq;
using UnityEngine;

/// <summary>
/// Init.
/// </summary>
public partial class MonitorBlockModuleController
{
  public override void Initialize(BlockControllerBase context)
  {
    base.Initialize(context);

    InitializeComponents();
  }

  private void InitializeComponents()
  {
    _animator = GetComponent<Animator>();

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
}
