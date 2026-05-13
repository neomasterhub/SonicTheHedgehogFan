using UnityEngine;

public interface IEnemy
{
  bool IsHit { get; set; }
  bool IsHurt { get; set; }
  bool IsImmortal { get; set; }
  bool IsAttacking { get; set; }
  GameObject LastHitSource { get; set; }
}
