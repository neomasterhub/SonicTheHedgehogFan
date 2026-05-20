using UnityEngine;

/// <summary>
/// Data.
/// </summary>
public partial class GroundEnemyAIController
  : AIControllerBase<GroundEnemyAIContext>,
  IEnemyAI
{
  [SerializeField]
  private float _maxPositionX;
  [SerializeField]
  private float _minPositionX;

  public EnemyAIType Type => EnemyAIType.Ground;
}
