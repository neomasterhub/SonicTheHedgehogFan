using UnityEngine;

/// <summary>
/// Data.
/// </summary>
public partial class GroundEnemyAIController : AIControllerBase<GroundEnemyAIContext>
{
  [SerializeField]
  private float _maxPositionX;
  [SerializeField]
  private float _minPositionX;
}
