using UnityEngine;

/// <summary>
/// Init.
/// </summary>
public partial class EnemyController
{
  public EnemyController()
  {
    _timerSystem = new();

    _effects = new();
    SetEffectPipeline();
  }

  private void Awake()
  {
    InitializeComponents();
    InitializeTimers();
  }

  public void Initialize(GameObject enemy)
  {
    _otherEnemyObj = enemy;
    _otherEnemy = _otherEnemyObj.GetComponent<IEnemy>();
    _otherEnemyCollider = _otherEnemyObj.GetComponent<BoxCollider2D>();

    _initialized = true;
  }

  private void InitializeComponents()
  {
    _collider = GetComponent<BoxCollider2D>();
    _spriteRenderer = GetComponent<SpriteRenderer>();

    if (_otherEnemyObj != null)
    {
      _otherEnemy = _otherEnemyObj.GetComponent<IEnemy>();
      _otherEnemyCollider = _otherEnemyObj.GetComponent<BoxCollider2D>();
    }
  }

  private void InitializeTimers()
  {
    _deadVisibleTimer = new Timer(1)
      .WhenCompleted(() => _spriteRenderer.enabled = false);
  }
}
