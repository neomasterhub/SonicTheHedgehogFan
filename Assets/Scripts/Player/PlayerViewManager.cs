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

  private PlayerViewInput _input;
  private IPlayerViewRotator _playerViewRotator;
  private float _groundedAnimatorParameterSpeed;
  private bool _isBalancing;
  private bool _isSkidding;

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
    _input = input;
    _isBalancing = _input.PlayerState.HasFlag(PlayerState.Balancing);
    _isSkidding = _input.PlayerState.HasFlag(PlayerState.Skidding);

    UpdateAnimator();
    RotateSprite();
  }

  private void UpdateAnimator()
  {
    var animatorParameterSpeed = 0f;
    if (_input.PlayerState.HasFlag(PlayerState.Grounded))
    {
      _groundedAnimatorParameterSpeed = Mathf.Abs(_playerSpeedManager.SpeedX);
      animatorParameterSpeed = _groundedAnimatorParameterSpeed;
    }
    else if (_input.PlayerState.HasFlag(PlayerState.Airborne))
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

  private void RotateSprite()
  {
    var rotatorInput = new PlayerViewRotatorInput(
      _input.GroundAngleDeg,
      _playerSpeedManager.GroundSpeed,
      _input.StandingStraightGroundSpeedZone,
      _input.PrevGroundSide,
      _input.PlayerState,
      _input.PrevPlayerState);

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

    if (_isSkidding)
    {
      return;
    }

    if (_isBalancing)
    {
      if (_input.GroundSensorIdApplied == SensorId.A)
      {
        _spriteRenderer.flipX = false;
      }
      else if (_input.GroundSensorIdApplied == SensorId.B)
      {
        _spriteRenderer.flipX = true;
      }
      else
      {
        throw _input.GroundSensorIdApplied.ArgumentOutOfRangeException();
      }

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
}
