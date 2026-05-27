using UnityEngine;

/// <summary>
/// Data.
/// </summary>
public partial class HorizontalExhaustSmokeAIEnemyModuleController
  : AIEnemyModuleControllerBase
{
  private bool _exhaustSmokeVisibility;
  private Animator _exhaustSmokeAnimator;
  private SpriteRenderer _exhaustSmokeSpriteRenderer;
  private Transform _exhaustSmokeTransform;
  private Vector3 _exhaustSmokePosition;

  [SerializeField]
  private Vector3 _exhaustSmokeOrigin;
}
