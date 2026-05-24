using UnityEngine;

/// <summary>
/// Behaviour.
/// </summary>
public class EnemyControllerBase
  : MonoBehaviour,
  IEnemyContext
{
  public bool IsInvincible { get; set; }
  public bool IsAttacking { get; set; }
  public bool IsHit { get; set; }
  public bool IsHurt { get; set; }
  public float SpeedX { get; set; }
  public float SpeedY { get; set; }
  public float PositionX { get; set; }
  public float PositionY { get; set; }
  public IEnemy ContactEnemy { get; set; }
  public bool IsStatic { get; set; }
  public bool HorizontalDirection { get; set; }
  public float Speed { get; set; }
  public float AnimatorSpeed { get; set; }
  public float WallClearance { get; set; }
  public GroundDetectionResult? Ground { get; set; }
  public WallDetectionResult? LeftWall { get; set; }
  public WallDetectionResult? RightWall { get; set; }
}
