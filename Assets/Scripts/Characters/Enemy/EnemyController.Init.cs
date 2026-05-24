using UnityEngine;

/// <summary>
/// Init.
/// </summary>
public partial class EnemyController
{
  public EnemyController()
  {
    IsStatic = true;
    AnimatorSpeed = 1;

    _effects = new();
    SetEffectPipeline();
  }

  private void Awake()
  {
    InitializeComponents();
  }

  private void InitializeComponents()
  {
    _collider = GetComponent<BoxCollider2D>();
    _modules = GetComponents<EnemyModuleControllerBase>();

    if (_otherEnemyObj != null)
    {
      _otherEnemy = _otherEnemyObj.GetComponent<IEnemy>();
      _otherEnemyCollider = _otherEnemyObj.GetComponent<BoxCollider2D>();
    }
  }
}
