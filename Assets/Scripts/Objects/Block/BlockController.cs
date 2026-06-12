using UnityEngine;

/// <summary>
/// Data.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public partial class BlockController : BlockControllerBase
{
  private readonly BlockSpeedSystem _speedSystem;
  private readonly Pipeline _effects;

  private Collider2D _collider;
  private Collider2D _playerCollider;
  private IBlockPlayer _player;
  private float _playerCombinedHRadius;
  private float _hDistanceToPlayer;

  [SerializeField]
  private float _pushSpeedPx;
}
