using UnityEngine;

public interface IPlatformObject
{
  Transform ContactGroundTransform { get; }
  IPlatform ContactPlatform { set; }
}
