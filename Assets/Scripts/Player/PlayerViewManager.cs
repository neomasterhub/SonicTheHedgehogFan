using UnityEngine;
using AnimatorParameters = Consts.Animator.Parameters;
using AnimatorStates = Consts.Animator.States;

public class PlayerViewManager
{
  private readonly Animator _animator;
  private readonly InputInfo _inputInfo;
  private readonly PlayerSpeedManager _playerSpeedManager;
  private readonly PlayerViewRotatorProvider _playerViewRotatorProvider;
  private readonly SpriteRenderer _spriteRenderer;

  private IPlayerViewRotator _playerViewRotator;

  public PlayerViewManager(
    Animator animator,
    InputInfo inputInfo,
    PlayerSpeedManager playerSpeedManager,
    PlayerViewRotatorProvider playerViewRotatorProvider,
    SpriteRenderer spriteRenderer)
  {
    _animator = animator;
    _inputInfo = inputInfo;
    _playerSpeedManager = playerSpeedManager;
    _playerViewRotatorProvider = playerViewRotatorProvider;
    _spriteRenderer = spriteRenderer;
  }

  public void Update(PlayerViewInput input)
  {
    RotateSprite(input);
    UpdateAnimator(input);
  }

  private void RotateSprite(PlayerViewInput input)
  {
    var rotatorInput = new PlayerViewRotatorInput(
      input.GroundAngleDeg,
      _playerSpeedManager.GroundSpeed,
      0.001f,
      input.PrevGroundSide,
      input.PlayerState,
      input.PrevPlayerState);

    var nextPlayerViewRotator = _playerViewRotatorProvider.FirstTriggered();
    if (nextPlayerViewRotator != null)
    {
      _playerViewRotator = nextPlayerViewRotator;
    }

    if (_playerViewRotator != null)
    {
      _playerViewRotator.Rotate(rotatorInput);
      _spriteRenderer.transform.localRotation = Quaternion.Euler(_playerViewRotator.Rotation);
    }

    if (_playerSpeedManager.IsSkidding)
    {
      return;
    }

    if (_inputInfo.X > 0)
    {
      _spriteRenderer.flipX = false;
    }
    else if (_inputInfo.X < 0)
    {
      _spriteRenderer.flipX = true;
    }
  }

  private void UpdateAnimator(PlayerViewInput input)
  {
    var animatorParSpeed = Mathf.Abs(_playerSpeedManager.SpeedX);
    if (input.PlayerState.HasFlag(PlayerState.Airborne))
    {
      animatorParSpeed = Mathf.Max(animatorParSpeed, input.AnimatorSpeedWalkingMin);
    }

    _animator.SetFloat(AnimatorParameters.Speed, animatorParSpeed);
    _animator.SetBool(AnimatorParameters.Skidding, _playerSpeedManager.IsSkidding);

    if (_animator.GetCurrentAnimatorStateInfo(0).IsName(AnimatorStates.Walking))
    {
      _animator.speed = Mathf.Max(
        input.AnimatorSpeedWalkingMin,
        animatorParSpeed / input.TopSpeed * input.AnimatorSpeedWalkingFactor);
    }
    else
    {
      _animator.speed = 1;
    }
  }
}
