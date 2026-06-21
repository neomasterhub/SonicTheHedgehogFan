using UnityEngine;

/// <summary>
/// Data.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public partial class OneShotDestructionBlockModule
  : BlockModuleControllerBase
{
  private readonly Pipeline _effects;

  private int _layer;
  private int _prevLayer;
  private bool _playerIsAttacking;
  private bool _playerIsIntersecting;
  private IBlockPlayer _player;
  private Collider2D _collider;
  private Collider2D _playerCollider;
}
