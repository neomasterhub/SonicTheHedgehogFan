using UnityEngine;

/// <summary>
/// Behaviour.
/// </summary>
public partial class SonicController
  : ICameraTarget,
  IBlockPlayer,
  IEnemy,
  ILookVerticalDirectionProvider,
  IPlatformObject,
  IRingCollector,
  ISceneObjectDebug
{
  public bool IsInvincible { get; private set; }
  public bool IsAttacking { get; private set; }
  public bool IsHit { get; private set; }
  public bool IsHurt { get; private set; }
  public bool IsDying => _isDying;
  public bool IsDead => _isDead;
  public int Health => Rings.Count > 0 ? 1 : 0;
  public float SpeedX => _speedSystem.SpeedX;
  public float SpeedY => _speedSystem.SpeedY;
  public float PositionX => transform.position.x;
  public float PositionY => transform.position.y;
  public IEnemy ContactEnemy { get; set; }

  public bool CanCollectRing { get; private set; }
  public ICollector Rings { get; }

  public VerticalDirection LookVerticalDirection
  {
    get
    {
      if (_isCurlingUp)
      {
        return VerticalDirection.Down;
      }

      if (_isLookingUp)
      {
        return VerticalDirection.Up;
      }

      return VerticalDirection.None;
    }
  }

  public Transform ContactGroundTransform => _contactGroundTransform;
  public IPlatform ContactPlatform { set => _contactPlatform = value; }

  public bool IsGrounded => _isGrounded;
  public bool PrevIsGrounded => _prevIsGrounded;
  public float BlockDetectionRadius => SonicConsts.Physics.BlockDetectionRadius;
  public float? HDistanceToBlock { set => _hDistanceToBlock = value; }
  public IBlock ContactBlock { get; set; }

  public bool DebugMode
  {
    set
    {
      _prevDebugMode = _debugMode;
      _debugMode = value;
    }
  }
}
