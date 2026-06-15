public interface IBlock
{
  float PushSpeed { get; }
  WallDetectionResult? LeftWall { get; }
  WallDetectionResult? RightWall { get; }
}
