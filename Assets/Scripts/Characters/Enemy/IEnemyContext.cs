public interface IEnemyContext : IEnemy
{
  float Speed { get; set; }
  WallDetectionResult? Wall { get; set; }
  GroundDetectionResult? Ground { get; set; }
}
