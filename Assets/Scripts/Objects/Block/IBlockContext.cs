public interface IBlockContext : IBlock
{
  float Speed { get; set; }
  IBlockPlayer Player { get; }
  GroundDetectionResult? Ground { get; set; }
}
