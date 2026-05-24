using UnityEngine;

/// <summary>
/// Init.
/// </summary>
public partial class NormalViewEnemyModuleController
{
  public override void Initialize(EnemyControllerBase context)
  {
    base.Initialize(context);
    InitializeComponents();
  }

  private void InitializeComponents()
  {
    _animator = GetComponent<Animator>();
    _spriteRenderer = GetComponent<SpriteRenderer>();
  }
}
