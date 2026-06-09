using UnityEngine;

/// <summary>
/// Data.
/// </summary>
public partial class LinearMovementPlatformModuleController
  : PlatformModuleControllerBase
{
  private readonly TimerSystem _timerSystem;

  private Timer _targetStopTimer;
  private Vector3 _target;
  private float _speed;
  private bool _isStopped;

  [SerializeField]
  private Vector3 _from;
  [SerializeField]
  private Vector3 _to;
  [SerializeField]
  private float _speedPx;
  [SerializeField]
  private float _targetStopDuration;
}
