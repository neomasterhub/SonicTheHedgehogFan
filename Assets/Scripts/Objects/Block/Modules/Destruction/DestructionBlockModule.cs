using UnityEngine;

/// <summary>
/// Data.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public partial class DestructionBlockModule
  : BlockModuleControllerBase
{
  private readonly Pipeline _effects;

  private int _layer;
  private int _prevLayer;
  private bool _playerIsAttacking;
  private Collider2D _collider;
  private Collider2D _playerCollider;
  private IBlockPlayer _player;

  [SerializeField]
  private float _minAttackPlayerGroundSpeed;
}
