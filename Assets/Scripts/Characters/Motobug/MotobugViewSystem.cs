using UnityEngine;
using AnimatorParameters = SharedConsts.Animator.Parameters;

public class MotobugViewSystem
{
  private Animator _animator;
  private MotobugViewContext _context;

  public void SetComponents(Animator animator)
  {
    _animator = animator;
  }

  public void Update(MotobugViewContext context)
  {
    _context = context;
    UpdateAnimator();
  }

  private void UpdateAnimator()
  {
    _animator.SetBool(AnimatorParameters.Destroyed, _context.Destroyed);
  }
}
