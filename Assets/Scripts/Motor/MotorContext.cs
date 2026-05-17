using UnityEngine;

public readonly struct MotorContext
{
  public readonly bool IsGrounded;
  public readonly bool PrevIsGrounded;
  public readonly float? GroundAngleRad;
  public readonly Vector2 Direction;

  private MotorContext(bool isGrounded, bool prevIsGrounded, float? groundAngleRad, Vector2 direction)
  {
    IsGrounded = isGrounded;
    PrevIsGrounded = prevIsGrounded;
    GroundAngleRad = groundAngleRad;
    Direction = direction;
  }

  public static MotorContext GetGrounded(bool prevIsGrounded, float groundAngleRad, Vector2 direction)
  {
    return new(true, prevIsGrounded, groundAngleRad, direction);
  }

  public static MotorContext GetAirborne(bool prevIsGrounded, Vector2 direction)
  {
    return new(false, prevIsGrounded, null, direction);
  }
}
