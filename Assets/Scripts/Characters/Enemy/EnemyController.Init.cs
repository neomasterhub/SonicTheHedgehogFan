using UnityEngine;

/// <summary>
/// Init.
/// </summary>
public partial class EnemyController
{
  public EnemyController()
  {
    _effects = new();
  }

  private void Awake()
  {
    _collider = GetComponent<BoxCollider2D>();
    _spriteRenderer = GetComponent<SpriteRenderer>();

    if (_enemy != null)
    {
      _enemyCollider = _enemy.GetComponent<BoxCollider2D>();
    }
  }

  public void Initialize(GameObject enemy)
  {
    _enemy = enemy;
    _enemyCollider = _enemy.GetComponent<BoxCollider2D>();

    _initialized = true;
  }
}
