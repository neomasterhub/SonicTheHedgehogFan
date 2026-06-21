/// <summary>
/// Data.
/// </summary>
public partial class OneShotDestructionBlockModule
  : BlockModuleControllerBase
{
  private readonly Pipeline _effects;

  private int _layer;
  private int _prevLayer;
  private bool _playerIsAttacking;
  private IBlockPlayer _player;
}
