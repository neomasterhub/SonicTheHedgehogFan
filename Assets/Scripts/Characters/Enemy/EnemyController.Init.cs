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

    if (_enemyGameObject != null)
    {
      _enemy = _enemyGameObject.GetComponent<IEnemy>();
      _enemyCollider = _enemyGameObject.GetComponent<BoxCollider2D>();
    }
  }

  public void Initialize(GameObject enemy)
  {
    _enemyGameObject = enemy;
    _enemy = _enemyGameObject.GetComponent<IEnemy>();
    _enemyCollider = _enemyGameObject.GetComponent<BoxCollider2D>();

    _initialized = true;
  }
}
