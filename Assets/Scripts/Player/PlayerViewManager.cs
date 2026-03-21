using UnityEngine;

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
    var speedXAbs = Mathf.Abs(_playerSpeedManager.SpeedX);

    _animator.SetFloat(Consts.Animator.Parameters.Speed, speedXAbs);
    _animator.SetBool(Consts.Animator.Parameters.Skidding, _playerSpeedManager.IsSkidding);

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
