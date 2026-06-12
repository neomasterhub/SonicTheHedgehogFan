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
    var player = GameObject.FindWithTag(Tags.Player);
    _player = player.GetComponent<IBlockPlayer>();
  }

  private void InitializeData()
  {
    PushSpeed = _pushSpeedPx / PxPerUnit;
  }
}
