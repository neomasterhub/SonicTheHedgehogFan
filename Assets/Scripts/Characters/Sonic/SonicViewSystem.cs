using UnityEngine;

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
    var animatorParameterSpeed = 0f;
    if (_input.PlayerState.HasFlag(SonicState.Grounded))
    {
      _groundedAnimatorParameterSpeed = Mathf.Abs(_input.SpeedX);
      animatorParameterSpeed = _groundedAnimatorParameterSpeed;
    }
    else if (_input.PlayerState.HasFlag(SonicState.Airborne))
    {
      animatorParameterSpeed = Mathf.Max(
        _groundedAnimatorParameterSpeed,
        _input.AnimatorParameterSpeedAirborneMin);
    }

    _animator.SetFloat(AnimatorParameters.Speed, animatorParameterSpeed);
    _animator.SetBool(AnimatorParameters.Balancing, _isBalancing);
    _animator.SetBool(AnimatorParameters.Skidding, _isSkidding);

    if (_animator.GetCurrentAnimatorStateInfo(0).IsName(AnimatorStates.Walking))
    {
      _animator.speed = Mathf.Max(
        _input.AnimatorSpeedWalkingMin,
        animatorParameterSpeed / _input.TopSpeed * _input.AnimatorSpeedWalkingFactor);
    }
    else
    {
      _animator.speed = 1;
    }
  }
}
