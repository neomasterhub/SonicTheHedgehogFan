using UnityEngine;
using static RingConsts.UI;
using static SharedConsts.Physics;
using AnimatorParameters = SharedConsts.Animator.Parameters;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class RingController : MonoBehaviour
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

  public RingController()
  {
    _configs = new(_physicsMode);
    _speedSystem = new(_configs);
    _sensorSystem = new();
  }

  private void OnDrawGizmos()
  {
    _sensorSystem.Draw();
  }

  private void Awake()
  {
    _animator = GetComponent<Animator>();
    _collider = GetComponent<BoxCollider2D>();
    _spriteRenderer = GetComponent<SpriteRenderer>();
    _playerCollider = _player.GetComponent<BoxCollider2D>();
    _playerRings = _player.GetComponent<IRingCollector>().Rings;
  }

  private void FixedUpdate()
  {
    if (_collected)
    {
      return;
    }

    CollectByPlayer();

    if (!_gravityEnabled)
    {
      return;
    }

    AnalyzeEnvironment();
    ApplyMovement();
    UpdatePosition();
  }

  private void CollectByPlayer()
  {
    if (_collected)
    {
      return;
    }

    if (_collider.bounds.Intersects(_playerCollider.bounds))
    {
      _collected = true;
      _playerRings.Add();
      _animator.SetTrigger(AnimatorParameters.Collected);
      _spriteRenderer.sortingOrder = SparkleOrderInLayer;
    }
  }

  private void AnalyzeEnvironment()
  {
    _configs.Update(_physicsMode);
    _sensorSystem.Update(transform.position);

    var ground = _sensorSystem.DetectGround(GroundLayer);

    if (ground != null)
    {
      _lastGroundDetectionResult = ground.Value;
      AnalyzeEnvironment_Grounded();
    }
    else
    {
      AnalyzeEnvironment_Airborne();
    }
  }

  private void AnalyzeEnvironment_Airborne()
  {
    _isGrounded = false;
    _speedContext = RingSpeedContext.GetAirborne();
  }

  private void AnalyzeEnvironment_Grounded()
  {
    _isGrounded = true;
    _speedContext = RingSpeedContext.GetGrounded(_lastGroundDetectionResult.Normal);
  }

  private void ApplyMovement()
  {
    _speedSystem.SetSpeed(_speedContext);
  }

  private void UpdatePosition()
  {
    var speedY = _speedSystem.SpeedY;

    if (_isGrounded)
    {
      speedY -= (_lastGroundDetectionResult.Distance
        * (int)_lastGroundDetectionResult.SensorGroundRelation)
        - GroundedPositionUpwardOffset;
    }

    transform.position += new Vector3(_speedSystem.SpeedX, speedY);
  }
}
