using UnityEngine;

public interface IEnemy
{
  bool IsInvincible { get; }
  bool IsAttacking { get; }
  bool IsHit { get; set; }
  bool IsHurt { get; set; }
  GameObject LastHitSource { get; set; }
}
