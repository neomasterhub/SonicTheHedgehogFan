using UnityEngine;

public interface IPlatformObject
{
  bool IsGrounded { get; }
  Collider2D Collider { get; }
  IPlatform ContactPlatform { set; }
}
