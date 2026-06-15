using UnityEngine;

public abstract class BlockControllerBase
  : MonoBehaviour,
  IBlockContext
{
  public float PushSpeed { get; set; }
  public GroundDetectionResult? Ground { get; set; }
  public WallDetectionResult? LeftWall { get; set; }
  public WallDetectionResult? RightWall { get; set; }
}
