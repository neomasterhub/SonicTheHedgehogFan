using UnityEngine;

/// <summary>
/// Init.
/// </summary>
public partial class OneShotDestructionBlockModule
{
  public OneShotDestructionBlockModule()
    : base()
  {
    _effects = new();
    SetEffectPipeline();
  }

  public override void Initialize(BlockControllerBase context)
  {
    base.Initialize(context);

    _collider = GetComponent<Collider2D>();

    _layer = gameObject.layer;

    _player = context.Player;
    _playerCollider = _player.Collider;

    var size = _collider.bounds.size;
    _context.HRadius = size.x / 2;
    _context.VRadius = size.y / 2;
  }
}
