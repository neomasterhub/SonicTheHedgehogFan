public interface IEnemyContext : IEnemy
{
  bool IsStatic { get; set; }
  float AccelerationSpeed { get; set; }
  float WallClearance { get; set; }
  GroundDetectionResult? Ground { get; set; }
  WallDetectionResult? LeftWall { get; set; }
  WallDetectionResult? RightWall { get; set; }
}
