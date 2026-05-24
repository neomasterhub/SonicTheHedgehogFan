using AnimatorParameters = SharedConsts.Animator.Parameters;

/// <summary>
/// Behaviour.
/// </summary>
public partial class NormalViewEnemyModuleController
{
  public override void Apply()
  {
    RotateSprite();
    UpdateAnimator();
  }

  private void RotateSprite()
  {
    _spriteRenderer.flipX = !_context.HorizontalDirection;
  }

  private void UpdateAnimator()
  {
    _animator.SetFloat(AnimatorParameters.Speed, _context.Speed);

    _animator.SetBool(AnimatorParameters.Dying, _context.IsDying);
    _animator.SetBool(AnimatorParameters.Hit, _context.IsHit);
    _animator.SetBool(AnimatorParameters.Static, _context.IsStatic);
  }
}
