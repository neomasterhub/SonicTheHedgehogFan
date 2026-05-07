using UnityEngine;
using static SharedConsts.UI;
using AnimatorParameters = SharedConsts.Animator.Parameters;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class RingController : MonoBehaviour
{
  private bool _collected;
  private Animator _animator;
  private BoxCollider2D _collider;
  private SpriteRenderer _spriteRenderer;

  [SerializeField]
  private BoxCollider2D _playerCollider;

  private void Awake()
  {
    _animator = GetComponent<Animator>();
    _collider = GetComponent<BoxCollider2D>();
    _spriteRenderer = GetComponent<SpriteRenderer>();
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
      _animator.SetTrigger(AnimatorParameters.Collected);
      _spriteRenderer.sortingOrder = PlayerOrderInLayer + 1;
    }
  }
}
