using UnityEngine;
using static SharedConsts;

/// <summary>
/// Init.
/// </summary>
public partial class EnemyController
{
  public EnemyController()
    : base()
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

    if (_otherEnemyObj == null)
    {
      _otherEnemyObj = GameObject.FindWithTag(Tags.Player);
    }

    _otherEnemy = _otherEnemyObj.GetComponent<IEnemy>();
    _otherEnemyCollider = _otherEnemyObj.GetComponent<BoxCollider2D>();
  }
}
