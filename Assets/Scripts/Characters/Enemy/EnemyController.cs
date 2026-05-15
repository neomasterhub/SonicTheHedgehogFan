using UnityEngine;

/// <summary>
/// Data.
/// </summary>
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public partial class EnemyController : MonoBehaviour
{
  private readonly Pipeline _effects;
  private readonly TimerSystem _timerSystem;

  private IEnemy _otherEnemy;
  private BoxCollider2D _collider;
  private BoxCollider2D _otherEnemyCollider;
  private SpriteRenderer _spriteRenderer;
  private Timer _deadVisibleTimer;

  [SerializeField]
  private bool _initialized;
  [SerializeField]
  [InspectorLabel("Other Enemy")]
  private GameObject _otherEnemyObj;
}
