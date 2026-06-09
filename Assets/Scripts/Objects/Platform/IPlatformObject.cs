using UnityEngine;

public interface IPlatformObject
{
  Transform ContactTransform { get; }
  IPlatform ContactPlatform { set; }
}
