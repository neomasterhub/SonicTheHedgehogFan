using UnityEngine;

public readonly struct RingSpeedContext
{
  public readonly bool IsGrounded;
  public readonly Vector2 Normal;

  private RingSpeedContext(bool isGrounded, Vector2 normal)
  {
    IsGrounded = isGrounded;
    Normal = normal;
  }

  public static RingSpeedContext GetGrounded(Vector2 normal)
  {
    return new(true, normal);
  }

  public static RingSpeedContext GetAirborne()
  {
    return new(false, default);
  }
}
