using UnityEngine;

/// <summary>
/// Data.
/// </summary>
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public partial class EnemyController : MonoBehaviour
{
  private readonly Pipeline _effects;

  private BoxCollider2D _collider;
  private BoxCollider2D _playerCollider;
  private SpriteRenderer _spriteRenderer;

  [SerializeField]
  private bool _initialized;
  [SerializeField]
  private GameObject _player;
}
