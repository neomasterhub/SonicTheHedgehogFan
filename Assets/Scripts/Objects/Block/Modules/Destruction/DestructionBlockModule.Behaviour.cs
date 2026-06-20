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

    var pSpeedX = _player.SpeedX;

    _playerIsAttacking =
      pSpeedX >= _minAttackPlayerSpeedX
      || pSpeedX <= -_minAttackPlayerSpeedX;
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
