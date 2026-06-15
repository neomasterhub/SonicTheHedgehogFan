using UnityEngine;

/// <summary>
/// Data.
/// </summary>
public partial class BlockController : BlockControllerBase
{
  private BlockModuleControllerBase[] _modules;
  private IBlockPlayer _player;

  [SerializeField]
  private float _leftPushSpeedPx;
  [SerializeField]
  private float _rightPushSpeedPx;
}
