public interface IEnemyContext : IEnemy
{
  WallDetectionResult? Wall { get; set; }
  GroundDetectionResult? Ground { get; set; }
}
