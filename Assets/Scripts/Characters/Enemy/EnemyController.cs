using UnityEngine;

/// <summary>
/// Data.
/// </summary>
[RequireComponent(typeof(BoxCollider2D))]
public abstract partial class EnemyController : MonoBehaviour
{
  protected readonly Pipeline _effects;
  protected readonly TimerSystem _timerSystem;
  protected readonly ISpeedSystem _speedSystem;

  protected BoxCollider2D _collider;
  protected BoxCollider2D _otherEnemyCollider;
  protected IEnemy _otherEnemy;
  protected Timer _deadActiveTimer;

  [SerializeField]
  protected bool _initialized;
  [SerializeField]
  [InspectorLabel("Alive")]
  protected bool _isAlive;
  [SerializeField]
  [InspectorLabel("Other Enemy")]
  protected GameObject _otherEnemyObj;
}
