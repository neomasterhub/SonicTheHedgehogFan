using UnityEngine;

/// <summary>
/// Data.
/// </summary>
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public partial class RingController : MonoBehaviour
{
  private readonly Pipeline _effects;
  private readonly RingConfigs _configs;
  private readonly RingSpeedSystem _speedSystem;
  private readonly ASensorSystem _sensorSystem;

  private bool _isGrounded;
  private bool _isCollected;
  private Animator _animator;
  private BoxCollider2D _collider;
  private BoxCollider2D _collectorCollider;
  private GroundDetectionResult _lastGroundDetectionResult;
  private IRingCollector _collector;
  private RingSpeedContext _speedContext;
  private SpriteRenderer _spriteRenderer;

  [SerializeField]
  private bool _initialized;
  [SerializeField]
  private bool _gravityEnabled;
  [SerializeField]
  private float _lifetime;
  [SerializeField]
  [InspectorLabel("Collector")]
  private GameObject _collectorObj;
  [SerializeField]
  private PhysicsMode _physicsMode;
  [SerializeField]
  private Vector2 _initialSpeed;
}
