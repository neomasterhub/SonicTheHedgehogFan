using UnityEngine;

/// <summary>
/// Data.
/// </summary>
[RequireComponent(typeof(BoxCollider2D))]
public partial class EnemyController : MonoBehaviour
{
  private readonly Pipeline _effects;
  private readonly TimerSystem _timerSystem;

  private BoxCollider2D _collider;
  private BoxCollider2D _otherEnemyCollider;
  private IEnemy _otherEnemy;
  private Timer _deadActiveTimer;

  [SerializeField]
  private bool _initialized;
  [SerializeField]
  [InspectorLabel("Alive")]
  private bool _isAlive;
  [SerializeField]
  [InspectorLabel("Other Enemy")]
  private GameObject _otherEnemyObj;
}
