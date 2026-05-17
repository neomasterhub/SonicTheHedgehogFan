using UnityEngine;

public readonly struct ContactEnemyInfo
{
  public readonly bool IsHit;
  public readonly Vector2 Speed;
  public readonly Vector2 Position;

  public ContactEnemyInfo(bool isHit, Vector2 speed, Vector2 position)
  {
    IsHit = isHit;
    Speed = speed;
    Position = position;
  }
}
