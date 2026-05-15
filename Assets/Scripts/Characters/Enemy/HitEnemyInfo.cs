using UnityEngine;

public readonly struct HitEnemyInfo
{
  public readonly Vector2 Position;
  public readonly Vector2 Speed;

  public HitEnemyInfo(Vector2 position, Vector2 speed)
  {
    Position = position;
    Speed = speed;
  }
}
