/// <summary>
/// Behaviour.
/// </summary>
public partial class OneShotDestructionBlockModule
{
  public override void Apply()
  {
    SetPlayerAttacking();
    _effects.Run();
  }

  private void SetPlayerAttacking()
  {
    _playerIsAttacking = _player.IsRolling
      && _player.ContactBlock == null;
  }
}
