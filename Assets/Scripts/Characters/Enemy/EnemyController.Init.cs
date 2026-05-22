using UnityEngine;
using static EnemyConsts.Physics;

/// <summary>
/// Init.
/// </summary>
public partial class EnemyController
{
  public EnemyController()
  {
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

  private void InitializeComponents()
  {
    _collider = GetComponent<BoxCollider2D>();
    _modules = GetComponents<IEnemyModule>();

    if (_otherEnemyObj != null)
    {
      _otherEnemy = _otherEnemyObj.GetComponent<IEnemy>();
      _otherEnemyCollider = _otherEnemyObj.GetComponent<BoxCollider2D>();
    }
  }

  private void InitializeTimers()
  {
    _deadActiveTimer = new Timer(DeadActiveTimer)
      .WhenCompleted(() => gameObject.SetActive(false));
  }
}
