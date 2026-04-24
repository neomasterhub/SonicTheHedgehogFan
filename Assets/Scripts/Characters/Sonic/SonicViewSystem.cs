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

  public SonicViewSystem(PlayerInputSystem inputSystem, PlayerViewRotatorProvider<SonicViewRotatorContext> rotatorProvider)
  {
    _inputSystem = inputSystem;
    _rotatorProvider = rotatorProvider;
  }

  public IPlayerViewRotator<SonicViewRotatorContext> Rotator { get; private set; }

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
    _animator.SetBool(AnimatorParameters.Idle, _context.IsZeroGroundSpeedProgressReached);
    _animator.SetBool(AnimatorParameters.Balancing, _context.IsBalancing);
    _animator.SetBool(AnimatorParameters.CurlingUp, _context.IsCurlingUp);
    _animator.SetBool(AnimatorParameters.LookingUp, _context.IsLookingUp);
    _animator.SetBool(AnimatorParameters.Rolling, _context.IsRolling);
    _animator.SetBool(AnimatorParameters.Skidding, _context.IsSkidding);
    _animator.SetFloat(AnimatorParameters.Speed, animatorParameterSpeed);

    var animatorState = _animator.GetCurrentAnimatorStateInfo(0);

    if (animatorState.IsName(AnimatorStates.Walking))
    {
      _animator.speed = Mathf.Max(
        WalkingSpeedMin,
        animatorParameterSpeed / TopSpeed * WalkingSpeedFactor);

      return;
    }

    if (animatorState.IsName(AnimatorStates.Rolling))
    {
      _animator.speed = Mathf.Max(
        RollingSpeedMin,
        animatorParameterSpeed / TopSpeed * RollingSpeedFactor);
    }
  }

  private void RotateSprite()
  {
    var nextRotator = _rotatorProvider.FirstTriggered();
    if (nextRotator != null)
    {
      Rotator = nextRotator;
    }

    if (Rotator != null)
    {
      Rotator.Rotate(SonicViewRotatorContext.FromViewContext(_context));
      _spriteRenderer.transform.localRotation = Quaternion.Euler(Rotator.Rotation);
    }

    if (_context.IsSkidding)
    {
      return;
    }

    if (_context.IsBalancing)
    {
      _spriteRenderer.flipX = _context.TriggeredGroundSensorSide.Value;
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
