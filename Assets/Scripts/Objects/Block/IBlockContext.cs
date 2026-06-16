public interface IBlockContext : IBlock
{
  float Speed { get; set; }
  float SpeedX { get; set; }
  float SpeedY { get; set; }
  GroundDetectionResult? Ground { get; set; }
}
