using UnityEngine;

public class PlayerViewManager
{
  private readonly Animator _animator;
  private readonly InputInfo _inputInfo;
  private readonly PlayerSpeedManager _playerSpeedManager;
  private readonly SpriteRenderer _spriteRenderer;

  public PlayerViewManager(
    Animator animator,
    InputInfo inputInfo,
    PlayerSpeedManager playerSpeedManager,
    SpriteRenderer spriteRenderer)
  {
    _animator = animator;
    _inputInfo = inputInfo;
    _playerSpeedManager = playerSpeedManager;
    _spriteRenderer = spriteRenderer;
  }

  public void Update(PlayerState playerState, PlayerViewInput input)
  {
    RotateSprite(input);
    UpdateAnimator(input);
  }

  private void RotateSprite(PlayerViewInput input)
  {
    if (input.IsSkidding)
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
    var speedXAbs = Mathf.Abs(_playerSpeedManager.SpeedX);

    _animator.SetFloat(Consts.Animator.Parameters.Speed, speedXAbs);
    _animator.SetBool(Consts.Animator.Parameters.Skidding, input.IsSkidding);

    if (_animator.GetCurrentAnimatorStateInfo(0).IsName(Consts.Animator.States.Walking))
    {
      _animator.speed = Mathf.Max(
        input.MinAnimatorWalkingSpeed,
        speedXAbs / input.TopSpeed * input.AnimatorWalkingSpeedFactor);
    }
    else
    {
      _animator.speed = 1f;
    }
  }
}
