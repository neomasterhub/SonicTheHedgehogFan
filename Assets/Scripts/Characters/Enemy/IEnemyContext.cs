public interface IEnemyContext : IEnemy
{
  bool IsStatic { get; set; }
  float AccelerationSpeed { get; set; }
  WallDetectionResult? Wall { get; set; }
  GroundDetectionResult? Ground { get; set; }
}
