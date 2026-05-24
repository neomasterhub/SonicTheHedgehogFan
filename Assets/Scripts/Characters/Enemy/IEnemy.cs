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
  float Health { get; }
  float MaxHealth { get; }
  float AttackDamage { get; }
  IEnemy ContactEnemy { get; set; }
}
