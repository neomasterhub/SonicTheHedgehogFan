using UnityEngine;
using AnimatorParameters = SharedConsts.Animator.Parameters;

public class AnimalViewSystem
{
  private Animator _animator;
  private AnimalViewContext _context;

  public void InitializeComponents(Animator animator)
  {
    _animator = animator;
  }

  public void Update(AnimalViewContext context)
  {
    _context = context;
    UpdateAnimator();
  }

  private void UpdateAnimator()
  {
    _animator.SetBool(AnimatorParameters.Running, _context.IsRunning);
    _animator.SetFloat(AnimatorParameters.SpeedY, _context.SpeedY);
  }
}
