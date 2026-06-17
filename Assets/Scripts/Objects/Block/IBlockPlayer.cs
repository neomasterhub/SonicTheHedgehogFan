using UnityEngine;

public interface IBlockPlayer
{
  Transform ContactLeftWallTransform { get; }
  Transform ContactRightWallTransform { get; }
  IBlock ContactBlock { get; set; }
  float SpeedX { get; }
  float SpeedY { get; }
  bool IsGrounded { get; }
  bool IsPushing { get; }
  bool IsRolling { get; }
}
