using UnityEngine;
using static SharedConsts.Physics;
using static SharedConsts.UI;
using AnimatorParameters = SharedConsts.Animator.Parameters;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class RingController : MonoBehaviour
{
  private const int _sparkleOrderInLayer = PlayerOrderInLayer + 1;

  private readonly RingConfigs _configs;
  private readonly RingSpeedSystem _speedSystem;
  private readonly RingSensorSystem _sensorSystem;

  private bool _collected;
  private Animator _animator;
  private BoxCollider2D _collider;
  private BoxCollider2D _playerCollider;
  private ICollector _playerRings;
  private PhysicsMode _physicsMode;
  private SpriteRenderer _spriteRenderer;

  [SerializeField]
  private GameObject _player;

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
    ApplyMovement();
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
      _spriteRenderer.sortingOrder = _sparkleOrderInLayer;
    }
  }

  private void ApplyMovement()
  {
    _configs.Update(_physicsMode);

    _sensorSystem.Update(transform.position);
    var ground = _sensorSystem.DetectGround(GroundLayer);

    _speedSystem.SetSpeed(ground == null
      ? RingSpeedContext.GetAirborn()
      : RingSpeedContext.GetGrounded(ground.Value.AngleRad));

    transform.position += new Vector3(_speedSystem.SpeedX, _speedSystem.SpeedY);
  }
}
