public interface IEnemy
{
  bool IsInvincible { get; }
  bool IsAttacking { get; }
  bool IsHit { get; }
  bool IsHurt { get; }
  float SpeedX { get; }
  float SpeedY { get; }
  float PositionX { get; }
  float PositionY { get; }
  IEnemy ContactEnemy { get; }
}
