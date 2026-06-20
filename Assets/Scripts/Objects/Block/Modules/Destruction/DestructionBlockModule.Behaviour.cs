using static SharedConsts.Physics;

/// <summary>
/// Behaviour.
/// </summary>
public partial class DestructionBlockModule
{
  public override void Apply()
  {
    SetPlayerAttacking();
    SetLayer();
    SetPlayerIntersecting();
    _effects.Run();
  }

  private void SetPlayerAttacking()
  {
    if (!_player.IsRolling
      || _player.ContactBlock != null)
    {
      _playerIsAttacking = false;
      return;
    }

    if (_player.IsGrounded)
    {
      var pGroundSpeed = _player.GroundSpeed;

      _playerIsAttacking =
        pGroundSpeed >= _minAttackPlayerGroundSpeed
        || pGroundSpeed <= -_minAttackPlayerGroundSpeed;
    }
    else
    {
      var pSpeedX = _player.SpeedX;
      var pSpeedY = _player.SpeedY;

      _playerIsAttacking =
        (pSpeedY <= -_minAttackPlayerAirSpeedY
        || pSpeedY >= _minAttackPlayerAirSpeedY)
        && (pSpeedX >= _minAttackPlayerAirSpeedX
        || pSpeedX <= -_minAttackPlayerAirSpeedX);
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

  private void SetPlayerIntersecting()
  {
    _playerIsIntersecting =
      _layer == 0
      && _collider.bounds.Intersects(_playerCollider.bounds);
  }

  private int GetLayer()
  {
    return _playerIsAttacking ? 0 : BlockLayerIndex;
  }
}
