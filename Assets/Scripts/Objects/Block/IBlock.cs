public interface IBlock
{
  bool IsHit { get; }
  bool IsHurt { get; }
  int Health { get; }
  float SpeedX { get; }
  float SpeedY { get; }
  float LeftPushSpeed { get; }
  float RightPushSpeed { get; }
  WallDetectionResult? LeftWall { get; }
  WallDetectionResult? RightWall { get; }
}
