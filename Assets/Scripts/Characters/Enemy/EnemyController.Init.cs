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
    _drawGizmos = () => { };
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
    _ai = GetComponent<IEnemyAI>();
    _collider = GetComponent<BoxCollider2D>();

    InitializeSystems();

    _drawGizmos = () =>
    {
      _sensorSystem.Draw();
    };

    if (_otherEnemyObj != null)
    {
      _otherEnemy = _otherEnemyObj.GetComponent<IEnemy>();
      _otherEnemyCollider = _otherEnemyObj.GetComponent<BoxCollider2D>();
    }
  }

  private void InitializeSystems()
  {
    var sensorSystem = GetComponent<IEnemySensorSystem>();
    _sensorSystem = sensorSystem.Type switch
    {
      EnemySensorSystemType.UFD => (UDFEnemySensorSystemController)GetComponent<IEnemySensorSystem>(),
      _ => throw sensorSystem.Type.ArgumentOutOfRangeException(),
    };
    _sensorSystem.SetNext(_ai);
  }

  private void InitializeTimers()
  {
    _deadActiveTimer = new Timer(DeadActiveTimer)
      .WhenCompleted(() => gameObject.SetActive(false));
  }
}
