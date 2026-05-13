using UnityEngine;

/// <summary>
/// Init.
/// </summary>
public partial class RingController : MonoBehaviour
{
  public RingController()
  {
    _lifetime = float.PositiveInfinity;

    _configs = new(_physicsMode);
    _speedSystem = new(_configs);
    _sensorSystem = new();

    _effects = new();
    SetEffectPipeline();
  }

  private void Awake()
  {
    _speedSystem.Initialize(_initialSpeed.x, _initialSpeed.y);

    _animator = GetComponent<Animator>();
    _collider = GetComponent<BoxCollider2D>();
    _spriteRenderer = GetComponent<SpriteRenderer>();

    if (_collectorObj != null)
    {
      _collector = _collectorObj.GetComponent<IRingCollector>();
      _collectorCollider = _collectorObj.GetComponent<BoxCollider2D>();
    }
  }

  public void Initialize(
    GameObject player,
    PhysicsMode physicsMode = PhysicsMode.Normal,
    float lifetime = float.PositiveInfinity,
    float speedX = 0,
    float speedY = 0)
  {
    _collectorObj = player;
    _collector = _collectorObj.GetComponent<IRingCollector>();
    _collectorCollider = _collectorObj.GetComponent<BoxCollider2D>();

    _physicsMode = physicsMode;
    _lifetime = lifetime;

    _speedSystem.Initialize(speedX, speedY);

    _initialized = true;
  }
}
