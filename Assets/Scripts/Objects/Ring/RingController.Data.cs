using UnityEngine;

/// <summary>
/// Data.
/// </summary>
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public partial class RingController : MonoBehaviour
{
  private readonly RingConfigs _configs;
  private readonly RingSpeedSystem _speedSystem;
  private readonly RingSensorSystem _sensorSystem;

  private bool _collected;
  private bool _isGrounded;
  private Animator _animator;
  private BoxCollider2D _collider;
  private BoxCollider2D _playerCollider;
  private GroundDetectionResult _lastGroundDetectionResult;
  private ICollector _playerRings;
  private RingSpeedContext _speedContext;
  private SpriteRenderer _spriteRenderer;

  [SerializeField]
  private bool _gravityEnabled;
  [SerializeField]
  private GameObject _player;
  [SerializeField]
  private PhysicsMode _physicsMode;
}
