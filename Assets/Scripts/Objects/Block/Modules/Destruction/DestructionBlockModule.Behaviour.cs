using static SharedConsts.Physics;

/// <summary>
/// Behaviour.
/// </summary>
public partial class DestructionBlockModule
{
  public override void Apply()
  {
    SetPlayerFlags();
    SetLayer();
    _effects.Run();
  }

  private void SetPlayerFlags()
  {
    if (!_player.IsRolling
      || _player.ContactBlock != null)
    {
      _playerIsAttacking = false;
      return;
    }

    if (_player.IsGrounded)
    {
      _playerIsAttacking =
        _player.GroundSpeed >= _minAttackPlayerGroundSpeed
        || _player.GroundSpeed <= -_minAttackPlayerGroundSpeed;
    }
    else
    {
      _playerIsAttacking =
        _player.SpeedY <= _topAttackPlayerAirSpeedY
        && (_player.SpeedX >= _minAttackPlayerAirSpeedX
        || _player.GroundSpeed <= -_minAttackPlayerAirSpeedX);
    }
  }

  private void SetLayer()
  {
    _prevLayer = _layer;
    _layer = GetLayer();

    if (_layer != _prevLayer)
    {
      gameObject.layer = _layer;
    }
  }

  private int GetLayer()
  {
    return BlockLayerIndex;
  }
}
