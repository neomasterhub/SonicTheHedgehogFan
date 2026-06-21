/// <summary>
/// Data.
/// </summary>
public partial class OneShotDestructionBlockModule
  : BlockModuleControllerBase
{
  private readonly Pipeline _effects;

  private bool _playerIsAttacking;
  private IBlockPlayer _player;
}
