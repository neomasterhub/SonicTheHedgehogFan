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

  public void Update()
  {
    RotateSprite();
    UpdateAnimator();
  }

  private void RotateSprite()
  {
    if (_inputInfo.X > 0)
    {
      _spriteRenderer.flipX = false;
    }
    else if (_inputInfo.X < 0)
    {
      _spriteRenderer.flipX = true;
    }
  }

  private void UpdateAnimator()
  {
    var speedXAbs = Mathf.Abs(_playerSpeedManager.SpeedX);

    _animator.SetFloat(AnimatorParameterNames.Speed, speedXAbs);
  }

  private static class AnimatorParameterNames
  {
    public const string Speed = nameof(Speed);
  }
}
