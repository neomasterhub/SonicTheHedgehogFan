using UnityEngine;
using static BlockConsts;
using static SharedConsts.ConvertValues;

/// <summary>
/// Init.
/// </summary>
public partial class DestructionBlockModule
  : BlockModuleControllerBase
{
  public DestructionBlockModule()
    : base()
  {
    _minAttackPlayerGroundSpeedPx = MinAttackPlayerGroundSpeedPx;
    _minAttackPlayerAirSpeedXPx = MinAttackPlayerAirSpeedXPx;
    _minAttackPlayerAirSpeedYPx = MinAttackPlayerAirSpeedYPx;

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

    _minAttackPlayerGroundSpeed = _minAttackPlayerGroundSpeedPx / PxPerUnit;
    _minAttackPlayerAirSpeedX = _minAttackPlayerAirSpeedXPx / PxPerUnit;
    _minAttackPlayerAirSpeedY = _minAttackPlayerAirSpeedYPx / PxPerUnit;
  }
}
