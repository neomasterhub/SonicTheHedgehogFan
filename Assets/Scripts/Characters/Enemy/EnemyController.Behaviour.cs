using System;

/// <summary>
/// Behaviour.
/// </summary>
public partial class EnemyController
{
  public bool IsInvincible { get; private set; }
  public bool IsAttacking { get; private set; }
  public bool IsHit { get; set; }
  public bool IsHurt { get; set; }
  public float SpeedX { get; private set; }
  public float SpeedY { get; private set; }
  public float PositionX { get; private set; }
  public float PositionY { get; private set; }
  public IEnemy ContactEnemy { get; set; }
  public bool IsStatic { get; set; }
  public float AccelerationSpeed { get; set; }
  public float WallClearance { get; set; }
  public GroundDetectionResult? Ground { get; set; }
  public WallDetectionResult? LeftWall { get; set; }
  public WallDetectionResult? RightWall { get; set; }

  public void SetEnemy(Action<IEnemy> setter)
  {
    setter(this);
  }
}
