using UnityEngine;

public abstract class PlatformControllerBase
  : MonoBehaviour,
  IPlatformContext
{
  public Vector3 Translation { get; protected set; }
}
