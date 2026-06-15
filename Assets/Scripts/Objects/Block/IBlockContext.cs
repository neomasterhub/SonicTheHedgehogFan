public interface IBlockContext : IBlock
{
  GroundDetectionResult? Ground { get; set; }
  WallDetectionResult? LeftWall { get; set; }
  WallDetectionResult? RightWall { get; set; }
}
