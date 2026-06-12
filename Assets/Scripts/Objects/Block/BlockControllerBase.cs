using UnityEngine;

public abstract class BlockControllerBase
  : MonoBehaviour,
  IBlockContext
{
  public float PositionX => transform.position.x;
  public float PushSpeed { get; internal set; }
}
