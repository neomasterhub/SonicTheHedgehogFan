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
}
