public interface IBlock
{
  float LeftPushSpeed { get; }
  float RightPushSpeed { get; }
  WallDetectionResult? LeftWall { get; }
  WallDetectionResult? RightWall { get; }
}
