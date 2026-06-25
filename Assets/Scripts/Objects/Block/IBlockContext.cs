public interface IBlockContext : IBlock
{
  bool IsDestroyed { get; set; }
  bool IsPushedUpIntersecting { get; set; }
  float Speed { get; set; }
  IBlockPlayer Player { get; }
  GroundDetectionResult? Ground { get; set; }
}
