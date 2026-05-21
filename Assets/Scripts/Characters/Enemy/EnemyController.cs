using UnityEngine;

/// <summary>
/// Data.
/// </summary>
[RequireComponent(typeof(BoxCollider2D))]
public partial class EnemyController : MonoBehaviour, IEnemy
{
  private readonly Pipeline _effects;
  private readonly TimerSystem _timerSystem;

  private BoxCollider2D _collider;
  private BoxCollider2D _otherEnemyCollider;
  private IEnemy _otherEnemy;
  private Timer _deadActiveTimer;

  [SerializeField]
  [InspectorLabel("Alive")]
  private bool _isAlive;
  [SerializeField]
  [InspectorLabel("Other Enemy")]
  private GameObject _otherEnemyObj;

  public bool IsInvincible { get; private set; }
  public bool IsAttacking { get; private set; }
  public bool IsHit { get; set; }
  public bool IsHurt { get; set; }
  public float SpeedX { get; private set; }
  public float SpeedY { get; private set; }
  public float PositionX => transform.position.x;
  public float PositionY => transform.position.y;
  public IEnemy ContactEnemy { get; set; }
}
