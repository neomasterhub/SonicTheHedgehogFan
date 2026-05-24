public interface IEnemyContext : IEnemy
{
  bool IsStatic { get; set; }
  bool HorizontalDirection { get; set; }
  float Speed { get; set; }
  float WallClearance { get; set; }
  GroundDetectionResult? Ground { get; set; }
  WallDetectionResult? LeftWall { get; set; }
  WallDetectionResult? RightWall { get; set; }
}
