using UnityEngine;
using static SonicConsts.Physics;
using static SonicConsts.View;
using AnimatorParameters = SharedConsts.Animator.Parameters;
using AnimatorStates = SharedConsts.Animator.States;

public class SonicViewSystem
{
  private readonly PlayerInputSystem _inputSystem;
  private readonly PlayerViewRotatorProvider<SonicViewRotatorContext> _rotatorProvider;

  private float _groundedAnimatorParameterSpeed;
  private Animator _animator;
  private SonicViewContext _context;
  private SpriteRenderer _spriteRenderer;
  private IPlayerViewRotator<SonicViewRotatorContext> _rotator;

  public SonicViewSystem(PlayerInputSystem inputSystem, PlayerViewRotatorProvider<SonicViewRotatorContext> rotatorProvider)
  {
    _inputSystem = inputSystem;
    _rotatorProvider = rotatorProvider;
  }

  public void SetComponents(Animator animator, SpriteRenderer spriteRenderer)
  {
    _animator = animator;
    _spriteRenderer = spriteRenderer;
  }

  public void Update(SonicViewContext context)
  {
    _context = context;
    UpdateAnimator();
    RotateSprite();
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
        AirborneSpeedMin);
    }

    _animator.speed = 1;
    _animator.SetFloat(AnimatorParameters.Speed, animatorParameterSpeed);
    _animator.SetBool(AnimatorParameters.Balancing, _context.IsBalancing);
    _animator.SetBool(AnimatorParameters.Skidding, _context.IsSkidding);

    if (_animator.GetCurrentAnimatorStateInfo(0).IsName(AnimatorStates.Walking))
    {
      _animator.speed = Mathf.Max(
        WalkingSpeedMin,
        animatorParameterSpeed / TopSpeed * WalkingSpeedFactor);
    }
  }

  private void RotateSprite()
  {
    var nextRotator = _rotatorProvider.FirstTriggered();
    if (nextRotator != null)
    {
      _rotator = nextRotator;
    }

    if (_rotator != null)
    {
      _rotator.Rotate(SonicViewRotatorContext.FromViewContext(_context));
      _spriteRenderer.transform.localRotation = Quaternion.Euler(_rotator.Rotation);
    }

    if (_context.IsSkidding)
    {
      return;
    }

    if (_inputSystem.X > 0)
    {
      _spriteRenderer.flipX = false;
    }
    else if (_inputSystem.X < 0)
    {
      _spriteRenderer.flipX = true;
    }
  }
}
