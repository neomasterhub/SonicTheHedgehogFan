using static SharedConsts.Rendering;
using AnimatorParameters = SharedConsts.Animator.Parameters;

/// <summary>
/// Behaviour.
/// </summary>
public partial class NormalViewEnemyModuleController
{
  public override void Apply()
  {
    UpdateSprite();
    UpdateAnimator();
  }

  private void UpdateSprite()
  {
    _spriteRenderer.flipX = !_context.HorizontalDirection;

    if (_context.IsHit && _context.IsDying)
    {
      _spriteRenderer.sortingOrder = PlayerOrderInLayer + 1;
    }
  }

  private void UpdateAnimator()
  {
    _animator.SetFloat(AnimatorParameters.Speed, _context.Speed);

    _animator.SetBool(AnimatorParameters.Dying, _context.IsDying);
    _animator.SetBool(AnimatorParameters.Hit, _context.IsHit);
    _animator.SetBool(AnimatorParameters.Static, _context.IsStatic);
  }
}
