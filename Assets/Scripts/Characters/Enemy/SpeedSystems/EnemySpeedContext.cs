using UnityEngine;

public readonly struct EnemySpeedContext
{
  public readonly float DirectionAngleDeg;
  public readonly float DirectionAngleRad;

  public EnemySpeedContext(float directionAngleDeg)
  {
    DirectionAngleDeg = directionAngleDeg;
    DirectionAngleRad = directionAngleDeg * Mathf.Deg2Rad;
  }
}
