using UnityEngine;

public static class EnemyConsts
{
  public static class Physics
  {
    public const float DeadActiveTimer = 1;
    public const float SpeedSpx = 128;
    public const float GravitySpeedSpx = 56;
    public const float MaxFallSpeedPx = 16;
    public const float PatrolRadius = 10;
    public const float PatrolStoppedTimer = 3;
    public static readonly Vector3 UDFLengths = new(0.2f, 0.1f, 0.5f);
  }
}
