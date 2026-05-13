using UnityEngine;

/// <summary>
/// Init.
/// </summary>
public partial class EnemyController
{
  public EnemyController()
  {
    _effects = new();
    SetEffectPipeline();
  }

  private void Awake()
  {
    _collider = GetComponent<BoxCollider2D>();
    _spriteRenderer = GetComponent<SpriteRenderer>();

    if (_otherEnemyGameObject != null)
    {
      _otherEnemy = _otherEnemyGameObject.GetComponent<IEnemy>();
      _otherEnemyCollider = _otherEnemyGameObject.GetComponent<BoxCollider2D>();
    }
  }

  public void Initialize(GameObject enemy)
  {
    _otherEnemyGameObject = enemy;
    _otherEnemy = _otherEnemyGameObject.GetComponent<IEnemy>();
    _otherEnemyCollider = _otherEnemyGameObject.GetComponent<BoxCollider2D>();

    _initialized = true;
  }
}
