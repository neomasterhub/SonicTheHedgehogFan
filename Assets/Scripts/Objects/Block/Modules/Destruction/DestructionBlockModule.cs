using UnityEngine;

/// <summary>
/// Data.
/// </summary>
public partial class DestructionBlockModule
  : BlockModuleControllerBase
{
  private readonly Pipeline _effects;

  private IBlockPlayer _player;

  [SerializeField]
  private float _minAttackPlayerSpeedX;
}
