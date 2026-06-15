using UnityEngine;
using static SharedConsts;
using static SharedConsts.ConvertValues;

/// <summary>
/// Init.
/// </summary>
public partial class BlockController
{
  private void Awake()
  {
    InitializeComponents();
    InitializeData();
  }

  private void InitializeComponents()
  {
    _modules = GetComponents<BlockModuleControllerBase>();

    var player = GameObject.FindWithTag(Tags.Player);
    _player = player.GetComponent<IBlockPlayer>();
  }

  private void InitializeData()
  {
    LeftPushSpeed = _leftPushSpeedPx / PxPerUnit;
    RightPushSpeed = _rightPushSpeedPx / PxPerUnit;
  }
}
