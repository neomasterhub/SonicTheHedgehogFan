public interface IBlockContext : IBlock
{
  bool IsPushedUpIntersecting { get; set; }
  float Speed { get; set; }
  IBlockPlayer Player { get; }
  GroundDetectionResult? Ground { get; set; }
}
