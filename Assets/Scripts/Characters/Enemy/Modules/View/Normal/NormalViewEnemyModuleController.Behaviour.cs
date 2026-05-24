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
  }
}
