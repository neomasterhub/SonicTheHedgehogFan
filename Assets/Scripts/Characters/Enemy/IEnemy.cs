public interface IEnemy
{
  bool IsInvincible { get; }
  bool IsAttacking { get; }
  bool IsHit { get; }
  bool IsHurt { get; }
  bool IsDying { get; }
  bool IsDead { get; }
  int Health { get; }
  float SpeedX { get; }
  float SpeedY { get; }
  float PositionX { get; }
  float PositionY { get; }
  IEnemy ContactEnemy { get; set; }
}
