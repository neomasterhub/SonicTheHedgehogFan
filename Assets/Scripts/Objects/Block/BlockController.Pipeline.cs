using static Helpers.Math;

/// <summary>
/// Pipeline.
/// </summary>
public partial class BlockController
{
  private void FixedUpdate()
  {
    BeginFrame();
    ApplyEffects();
    ApplyMovement();
    UpdatePosition();
  }

  private void BeginFrame()
  {
    UpdateHDistanceToPlayer();
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
  }

  private void UpdateHDistanceToPlayer()
  {
    var x = transform.position.x;

    _hDistanceToPlayer = x - _player.PositionX
      + (x > _player.PositionX ? -_playerCombinedHRadius : _playerCombinedHRadius);
  }
}
