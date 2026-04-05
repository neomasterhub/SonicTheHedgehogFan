using UnityEngine;
using static SonicConsts.Physics;
using static SonicConsts.View;
using AnimatorParameters = SharedConsts.Animator.Parameters;
using AnimatorStates = SharedConsts.Animator.States;

public class SonicViewSystem
{
  private readonly Animator _animator;
  private readonly PlayerInputSystem _inputSystem;
  private readonly SpriteRenderer _spriteRenderer;

  private SonicViewContext _context;
  private float _groundedAnimatorParameterSpeed;

  public SonicViewSystem(Animator animator, PlayerInputSystem inputSystem, SpriteRenderer spriteRenderer)
  {
    _animator = animator;
    _inputSystem = inputSystem;
    _spriteRenderer = spriteRenderer;
  }

  public void Update(SonicViewContext context)
  {
    _context = context;
    UpdateAnimator();
  }

  private void UpdateAnimator()
  {
    float animatorParameterSpeed;
    if (_context.IsGrounded)
    {
      _groundedAnimatorParameterSpeed = Mathf.Abs(_context.SpeedX);
      animatorParameterSpeed = _groundedAnimatorParameterSpeed;
    }
    else
    {
      animatorParameterSpeed = Mathf.Max(
        _groundedAnimatorParameterSpeed,
        SpeedAirborneMin);
    }

    _animator.speed = 1;
    _animator.SetFloat(AnimatorParameters.Speed, animatorParameterSpeed);
    _animator.SetBool(AnimatorParameters.Balancing, _context.IsBalancing);
    _animator.SetBool(AnimatorParameters.Skidding, _context.IsSkidding);

    if (_animator.GetCurrentAnimatorStateInfo(0).IsName(AnimatorStates.Walking))
    {
      _animator.speed = Mathf.Max(
        SpeedWalkingMin,
        animatorParameterSpeed / TopSpeed * SpeedWalkingFactor);
    }
  }
}
