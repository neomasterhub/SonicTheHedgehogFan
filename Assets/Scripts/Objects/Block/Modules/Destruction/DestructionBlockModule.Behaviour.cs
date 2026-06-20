using static SharedConsts.Physics;

/// <summary>
/// Behaviour.
/// </summary>
public partial class DestructionBlockModule
{
  public override void Apply()
  {
    SetLayer();
    _effects.Run();
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
    if (!_player.IsRolling
      || _player.ContactBlock != null)
    {
      return BlockLayerIndex;
    }

    var pSpeedX = _player.SpeedX;

    if (_player.IsGrounded)
    {
      return pSpeedX >= _minAttackPlayerSpeedX
        || pSpeedX <= -_minAttackPlayerSpeedX
        ? 0
        : BlockLayerIndex;
    }

    return BlockLayerIndex;
  }
}
