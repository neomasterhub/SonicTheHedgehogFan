using UnityEngine;

/// <summary>
/// Data.
/// </summary>
public partial class GroundSpeedBlockModuleController
  : BlockModuleControllerBase
{
  private const float _clearance = 0.01f;

  private bool _isPushedUp;
  private float _gravitySpeed;
  private float _maxFallSpeed;
  private float _minPushUpSpeed;
  private IBlockPlayer _player;

  [SerializeField]
  private bool _fallAfterPushedUp;
  [SerializeField]
  private float _gravitySpeedSpx;
  [SerializeField]
  private float _maxFallSpeedPx;
  [SerializeField]
  private float _minPushUpSpeedPx;
}
