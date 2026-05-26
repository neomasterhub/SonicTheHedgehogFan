/// <summary>
/// Behaviour.
/// </summary>
public partial class SonicController
{
  // Camera target
  public bool IsDead => _isDead;
  public bool IsDying => _isDying;

  // Enemy
  public bool IsInvincible { get; private set; }
  public bool IsAttacking { get; private set; }
  public bool IsHit { get; private set; }
  public bool IsHurt { get; private set; }
  public int Health { get; }
  public float SpeedX => _speedSystem.SpeedX;
  public float SpeedY => _speedSystem.SpeedY;
  public float PositionX => transform.position.x;
  public float PositionY => transform.position.y;
  public IEnemy ContactEnemy { get; set; }

  // Look vertical direction provider
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

  // Ring collector
  public bool CanCollectRing { get; private set; }
  public ICollector Rings { get; }
}
