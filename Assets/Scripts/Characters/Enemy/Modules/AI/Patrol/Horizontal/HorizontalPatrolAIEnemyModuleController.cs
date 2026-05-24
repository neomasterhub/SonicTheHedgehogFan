using UnityEngine;

/// <summary>
/// Data.
/// </summary>
public partial class HorizontalPatrolAIEnemyModuleController
  : AIEnemyModuleControllerBase
{
  private bool _isStopped;
  private float _speed;
  private Timer _stoppedTimer;

  [SerializeField]
  private float _speedSpx;
  [SerializeField]
  private float _stopTimer;
  [SerializeField]
  private float _minPositionX;
  [SerializeField]
  private float _maxPositionX;
}
