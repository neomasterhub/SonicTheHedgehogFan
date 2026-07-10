using UnityEngine;
using static RingConsts.Physics;

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
    _sensorSystem = new(new(0, SensorY), Color.gold);

    _effects = new();
    SetEffectPipeline();
  }

  private void Awake()
  {
    _animator = GetComponent<Animator>();
    _collider = GetComponent<BoxCollider2D>();
    _spriteRenderer = GetComponent<SpriteRenderer>();
    _meshRenderer = GameObject.Find("Mesh Renderer").GetComponent<IMeshRenderer>();

    _sensorSystem.SetMeshRenderer(_meshRenderer);
    _speedSystem.Initialize(_initialSpeed.x, _initialSpeed.y);

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
