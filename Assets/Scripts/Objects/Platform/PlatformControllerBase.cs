using UnityEngine;

public abstract class PlatformControllerBase
  : MonoBehaviour,
  IPlatformContext
{
  public Vector3 Displacement { get; protected set; }
}
