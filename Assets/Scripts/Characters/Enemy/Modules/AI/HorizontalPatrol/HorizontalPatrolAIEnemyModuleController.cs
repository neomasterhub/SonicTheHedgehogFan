using UnityEngine;

/// <summary>
/// Data.
/// </summary>
public partial class HorizontalPatrolAIEnemyModuleController
  : AIEnemyModuleControllerBase
{
  private bool _isStopped;
  private Timer _stopTimer;

  [SerializeField]
  private float _speed;
  [SerializeField]
  private float _stopDuration;
  [SerializeField]
  private float _minPositionX;
  [SerializeField]
  private float _maxPositionX;
}
