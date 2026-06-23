using UnityEngine;

public abstract class BlockControllerBase
  : MonoBehaviour,
  IBlockContext
{
  public bool IsHit { get; set; }
  public bool IsHurt { get; set; }
  public bool IsPushedUpIntersecting { get; set; }
  public int Health { get; set; }
  public float HRadius { get; set; }
  public float VRadius { get; set; }
  public float Speed { get; set; }
  public float PositionX { get; set; }
  public float PositionY { get; set; }
  public float SpeedX { get; set; }
  public float SpeedY { get; set; }
  public float LeftPushSpeed { get; set; }
  public float RightPushSpeed { get; set; }
  public GroundDetectionResult? Ground { get; set; }
  public WallDetectionResult? LeftWall { get; set; }
  public WallDetectionResult? RightWall { get; set; }
  public IBlockPlayer Player { get; protected set; }
}
