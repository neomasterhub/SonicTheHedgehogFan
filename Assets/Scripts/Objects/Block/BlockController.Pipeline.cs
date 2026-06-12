using UnityEngine;
using static Helpers.Math;

/// <summary>
/// Pipeline.
/// </summary>
public partial class BlockController
{
  private void FixedUpdate()
  {
    AnalyzeEnvironment();
    ApplyEffects();
    ApplyMovement();
    UpdatePosition();
  }

  private void AnalyzeEnvironment()
  {
    _hDistanceToPlayer = GetHDistanceToPlayer();
  }

  private void ApplyEffects()
  {
    _effects.Run();
  }

  private void ApplyMovement()
  {
    _speedSystem.SetSpeed(new(transform.position.x));
  }

  private void UpdatePosition()
  {
    transform.position += PositionVector3(_speedSystem.SpeedX, _speedSystem.SpeedY);

    _hDistanceToPlayer = GetHDistanceToPlayer();

    if (Mathf.Abs(_hDistanceToPlayer) < _player.BlockDetectionRadius)
    {
      _player.HDistanceToBlock = _hDistanceToPlayer;
    }
  }

  private float GetHDistanceToPlayer()
  {
    var x = transform.position.x;

    return _hDistanceToPlayer = x - _player.PositionX
      + (_player.PositionX < x ? -_playerCombinedHRadius : _playerCombinedHRadius);
  }
}
