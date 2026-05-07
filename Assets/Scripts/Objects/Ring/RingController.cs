using UnityEngine;
using static SharedConsts.Animator.Parameters;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]
public class RingController : MonoBehaviour
{
  private bool _collected;
  private Animator _animator;
  private BoxCollider2D _collider;

  [SerializeField]
  private BoxCollider2D _playerCollider;

  private void Awake()
  {
    _animator = GetComponent<Animator>();
    _collider = GetComponent<BoxCollider2D>();
  }

  private void FixedUpdate()
  {
    if (_collected)
    {
      return;
    }

    if (_collider.bounds.Intersects(_playerCollider.bounds))
    {
      _collected = true;
      _animator.SetTrigger(Collected);
    }
  }
}
