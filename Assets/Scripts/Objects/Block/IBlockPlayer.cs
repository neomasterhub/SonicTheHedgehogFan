using UnityEngine;

public interface IBlockPlayer
{
  Transform ContactCeilingTransform { get; }
  Transform ContactGroundTransform { get; }
  Transform ContactLeftWallTransform { get; }
  Transform ContactRightWallTransform { get; }
  IBlock ContactBlock { get; set; }
  Collider2D Collider { get; }
  float HRadius { get; }
  float VRadius { get; }
  float PositionX { get; }
  float PositionY { get; }
  float SpeedX { get; }
  float SpeedY { get; }
  float GroundSpeed { get; }
  bool IsGrounded { get; }
  bool IsPushing { get; }
  bool IsRolling { get; }
  bool IsStoppedByCeiling { set; }
  bool ShieldReceived { set; }
  ReboundSignal ReboundSignal { set; }
}
