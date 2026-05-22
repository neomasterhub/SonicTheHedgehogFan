public interface IEnemyContext : IEnemy
{
  bool IsStatic { get; set; }
  float Speed { get; set; }
  WallDetectionResult? Wall { get; set; }
  GroundDetectionResult? Ground { get; set; }
}
