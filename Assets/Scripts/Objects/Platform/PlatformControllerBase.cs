using UnityEngine;

public abstract class PlatformControllerBase
  : MonoBehaviour,
  IPlatformContext
{
  public float SpeedX { get; set; }
  public float SpeedY { get; set; }
  public float PositionX { get; set; }
  public float PositionY { get; set; }
}
