using UnityEngine;
using static SharedConsts.UI;
using AnimatorParameters = SharedConsts.Animator.Parameters;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class RingController : MonoBehaviour
{
  private const int _sparkleOrderInLayer = PlayerOrderInLayer + 1;

  private readonly RingSpeedSystem _speedSystem;
  private readonly RingSensorSystem _sensorSystem;

  private bool _collected;
  private Animator _animator;
  private ICollector _playerRings;
  private BoxCollider2D _collider;
  private BoxCollider2D _playerCollider;
  private SpriteRenderer _spriteRenderer;

  [SerializeField]
  private GameObject _player;

  public RingController()
  {
    _speedSystem = new();
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
    _sensorSystem.Update(transform.position);
    _speedSystem.SetSpeed(_sensorSystem.DetectGround());
    transform.position += new Vector3(_speedSystem.SpeedX, _speedSystem.SpeedY);
  }
}
