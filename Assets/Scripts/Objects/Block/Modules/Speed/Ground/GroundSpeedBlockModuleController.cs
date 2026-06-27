using UnityEngine;

/// <summary>
/// Data.
/// </summary>
public partial class GroundSpeedBlockModuleController
  : BlockModuleControllerBase
{
  private bool _isPushedUp;
  private float _gravitySpeed;
  private float _maxFallSpeed;
  private float _pushUpSpeed;
  private IBlockPlayer _player;

  [SerializeField]
  private bool _fallAfterPushedUp;
  [SerializeField]
  private float _gravitySpeedSpx;
  [SerializeField]
  private float _maxFallSpeedPx;
  [SerializeField]
  private float _pushUpSpeedPx;
  [SerializeField]
  private float _hitboxVRadius;
}
