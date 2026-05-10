using UnityEngine;

/// <summary>
/// Init.
/// </summary>
public partial class RingController : MonoBehaviour
{
  public RingController()
  {
    _configs = new(_physicsMode);
    _speedSystem = new(_configs);
    _sensorSystem = new();
  }

  private void Awake()
  {
    _animator = GetComponent<Animator>();
    _collider = GetComponent<BoxCollider2D>();
    _spriteRenderer = GetComponent<SpriteRenderer>();

    if (_player != null)
    {
      _playerCollider = _player.GetComponent<BoxCollider2D>();
      _playerRings = _player.GetComponent<IRingCollector>().Rings;
    }
  }

  public void Initialize(
    GameObject player,
    PhysicsMode physicsMode = PhysicsMode.Normal,
    Vector3? position = null,
    float speedX = 0,
    float speedY = 0)
  {
    _player = player;
    _playerCollider = _player.GetComponent<BoxCollider2D>();
    _playerRings = _player.GetComponent<IRingCollector>().Rings;

    _speedSystem.Initialize(speedX, speedY);

    _physicsMode = physicsMode;

    transform.position = position ?? _player.transform.position;

    _initialized = true;
  }
}
