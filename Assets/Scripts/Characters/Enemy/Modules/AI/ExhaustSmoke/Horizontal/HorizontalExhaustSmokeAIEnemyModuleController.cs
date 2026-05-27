using UnityEngine;

/// <summary>
/// Data.
/// </summary>
public partial class HorizontalExhaustSmokeAIEnemyModuleController
  : AIEnemyModuleControllerBase
{
  private Vector3 _exhaustSmokePosition;
  private Animator _exhaustSmokeAnimator;
  private Transform _exhaustSmokeTransform;

  [SerializeField]
  private Vector3 _exhaustSmokeOrigin;
}
