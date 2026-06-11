using UnityEngine;
using static SharedConsts;

/// <summary>
/// Init.
/// </summary>
public partial class BlockController
{
  public BlockController()
    : base()
  {
    _effects = new();
    SetEffectPipeline();
  }

  private void Awake()
  {
    InitializeComponents();
  }

  private void InitializeComponents()
  {
    _collider = GetComponent<Collider2D>();

    var playerObj = GameObject.FindWithTag(Tags.Player);
    _player = playerObj.GetComponent<IBlockPlayer>();
    _playerCollider = playerObj.GetComponent<Collider2D>();
  }
}
