using UnityEngine;

/// <summary>
/// Init.
/// </summary>
public partial class DestructionBlockModule
  : BlockModuleControllerBase
{
  public DestructionBlockModule()
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
  }
}
