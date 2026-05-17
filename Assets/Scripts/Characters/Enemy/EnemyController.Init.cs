using UnityEngine;
using static EnemyConsts;

/// <summary>
/// Init.
/// </summary>
public partial class EnemyController
{
  public EnemyController(ISpeedSystem speedSystem)
  {
    _speedSystem = speedSystem;

    _isAlive = true;

    _timerSystem = new();

    _effects = new();
    SetEffectPipeline();
  }

  private void Awake()
  {
    InitializeComponents();
    InitializeTimers();
  }

  protected virtual void Initialize(GameObject enemy)
  {
    _otherEnemyObj = enemy;
    _otherEnemy = _otherEnemyObj.GetComponent<IEnemy>();
    _otherEnemyCollider = _otherEnemyObj.GetComponent<BoxCollider2D>();

    _initialized = true;
  }

  protected virtual void InitializeComponents()
  {
    _collider = GetComponent<BoxCollider2D>();

    if (_otherEnemyObj != null)
    {
      _otherEnemy = _otherEnemyObj.GetComponent<IEnemy>();
      _otherEnemyCollider = _otherEnemyObj.GetComponent<BoxCollider2D>();
    }
  }

  protected virtual void InitializeTimers()
  {
    _deadActiveTimer = new Timer(DeadActiveTimer)
      .WhenCompleted(() => gameObject.SetActive(false));
  }
}
