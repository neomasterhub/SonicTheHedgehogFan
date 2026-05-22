using UnityEngine;

/// <summary>
/// Data.
/// </summary>
public partial class HorizontalPatrolAIEnemyModuleController
  : AIEnemyModuleControllerBase
{
  [SerializeField]
  private float _speed;
  [SerializeField]
  private float _minPositionX;
  [SerializeField]
  private float _maxPositionX;
}
