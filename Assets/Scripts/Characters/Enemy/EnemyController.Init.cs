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

    if (_player != null)
    {
      _playerCollider = _player.GetComponent<BoxCollider2D>();
    }
  }

  public void Initialize(GameObject player)
  {
    _player = player;
    _playerCollider = _player.GetComponent<BoxCollider2D>();

    _initialized = true;
  }
}
