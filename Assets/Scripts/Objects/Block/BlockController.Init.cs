using UnityEngine;
using static SharedConsts;
using static SharedConsts.ConvertValues;

/// <summary>
/// Init.
/// </summary>
public partial class BlockController
{
  public BlockController()
    : base()
  {
    _effects = new();
    _speedSystem = new();

    SetEffectPipeline();
  }

  private void Awake()
  {
    InitializeComponents();
    InitializeSystems();
    InitializeData();
  }

  private void InitializeComponents()
  {
    _collider = GetComponent<Collider2D>();

    var playerObj = GameObject.FindWithTag(Tags.Player);
    _player = playerObj.GetComponent<IBlockPlayer>();
    _playerCollider = playerObj.GetComponent<Collider2D>();
  }

  private void InitializeSystems()
  {
    _speedSystem.Initialize(_player);
  }

  private void InitializeData()
  {
    PushSpeed = _pushSpeedPx / PxPerUnit;
    _playerCombinedHRadius = (_collider.bounds.size.x + _playerCollider.bounds.size.x) / 2;
  }
}
