using UnityEngine;
using static SharedConsts;

/// <summary>
/// Init.
/// </summary>
public partial class BlockController
{
  private void Awake()
  {
    InitializeComponents();
  }

  private void InitializeComponents()
  {
    var player = GameObject.FindWithTag(Tags.Player);
    _player = player.GetComponent<IBlockPlayer>();
  }
}
