using UnityEngine;

/// <summary>
/// Data.
/// </summary>
public partial class HorizontalExhaustSmokeAIEnemyModuleController
  : AIEnemyModuleControllerBase
{
  private Animator _exhaustSmokeAnimator;
  private Transform _exhaustSmokeTransform;

  [SerializeField]
  private Vector2 _exhaustSmokeOrigin;
}
