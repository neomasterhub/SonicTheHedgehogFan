using UnityEngine;

public interface IPlatformObject
{
  bool IsGrounded { get; }
  Collider2D Collider { get; }
  float PlatformSpeedX { set; }
  float PlatformSpeedY { set; }
}
