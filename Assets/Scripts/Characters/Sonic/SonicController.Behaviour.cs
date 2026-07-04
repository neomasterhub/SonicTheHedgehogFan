using UnityEngine;
using static SharedConsts.Sounds;
using static SonicConsts.Sizes;

/// <summary>
/// Behaviour.
/// </summary>
public partial class SonicController
  : IBlockPlayer,
  ICameraTarget,
  IEnemy,
  ILookVerticalDirectionProvider,
  IPlatformObject,
  IRingCollector,
  ISceneObjectDebug,
  ISceneObjectPlayer
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

  public Transform ContactCeilingTransform => _contactCeilingTransform;
  public Transform ContactGroundTransform => _contactGroundTransform;
  public IPlatform ContactPlatform { set => _contactPlatform = value; }

  public Transform ContactLeftWallTransform => _contactLeftWallTransform;
  public Transform ContactRightWallTransform => _contactRightWallTransform;
  public IBlock ContactBlock { get; set; }
  public Collider2D Collider => _boxCollider;
  public bool IsGrounded => _isGrounded;
  public bool IsPushing => _speedSystem.IsPushing;
  public bool IsRolling => _isRolling;
  public bool IsStoppedByCeiling { set => _isStoppedByCeiling = value; }

  public bool RingReceived
  {
    set
    {
      _isGettingRingFromMonitor = true;
      _ringAudioSource.PlayDelayed(MonitorEffectDelay);
    }
  }

  public bool ShieldReceived
  {
    set
    {
      if (_hasShield)
      {
        return;
      }

      _hasShield = true;
      _isGettingShieldFromMonitor = true;
      _shield.SetActive(true);
      _shieldAudioSource.PlayDelayed(MonitorEffectDelay);
    }
  }

  public bool SpeedShoesReceived
  {
    set
    {
      _hasSpeedShoes = true;
      _timerSystem.Remove(_speedShoesTimer);
      _timerSystem.StartIfNotRunning(_speedShoesTimer);
    }
  }

  public bool InvincibilityStarsReceived
  {
    set
    {
      _hasInvincibilityStars = true;
      _invincibilityStars.SetActive(true);
      _timerSystem.Remove(_postHurtInvincibleTimer);
      _timerSystem.Remove(_invincibilityStarsTimer);
      _timerSystem.StartIfNotRunning(_invincibilityStarsTimer);

      IsAttacking = true;
      IsInvincible = true;
    }
  }

  public ReboundSignal ReboundSignal { set => _reboundSignal = value; }
  public float HRadius => _sizeMode == SonicSizeMode.Big ? Big.HRadius : Small.HRadius;
  public float VRadius => _sizeMode == SonicSizeMode.Big ? Big.VRadius : Small.VRadius;
  public float GroundSpeed => _speedSystem.GroundSpeed;

  public bool DebugMode
  {
    set
    {
      _prevDebugMode = _debugMode;
      _debugMode = value;
    }
  }

  public bool HasSpeedShoes => _hasSpeedShoes;
  public bool HasInvincibilityStars => _hasInvincibilityStars;
}
