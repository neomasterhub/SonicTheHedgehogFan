using UnityEngine;

/// <summary>
/// Data.
/// </summary>
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public partial class NormalViewEnemyModuleController
  : EnemyModuleControllerBase
{
  private Animator _animator;
  private SpriteRenderer _spriteRenderer;
}
