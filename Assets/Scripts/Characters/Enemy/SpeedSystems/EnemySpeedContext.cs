using UnityEngine;

public readonly struct EnemySpeedContext
{
  public readonly float Speed;
  public readonly float DirectionAngleDeg;
  public readonly float DirectionAngleRad;

  public EnemySpeedContext(float speed, float directionAngleDeg)
  {
    Speed = speed;
    DirectionAngleDeg = directionAngleDeg;
    DirectionAngleRad = directionAngleDeg * Mathf.Deg2Rad;
  }
}
