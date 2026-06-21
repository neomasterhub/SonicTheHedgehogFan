using static SharedConsts.Physics;

/// <summary>
/// Behaviour.
/// </summary>
public partial class OneShotDestructionBlockModule
{
  public override void Apply()
  {
    SetPlayerAttacking();
    SetLayer();
    _effects.Run();
  }

  private void SetPlayerAttacking()
  {
    _playerIsAttacking = _player.IsRolling
      && _player.ContactBlock == null;
  }

  private void SetLayer()
  {
    _prevLayer = _layer;
    _layer = _playerIsAttacking ? 0 : BlockLayerIndex;

    if (_layer != _prevLayer)
    {
      gameObject.layer = _layer;
    }
  }
}
