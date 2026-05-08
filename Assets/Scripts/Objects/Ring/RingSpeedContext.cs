public readonly struct RingSpeedContext
{
  public readonly bool IsGrounded;
  public readonly float? GroundAngleRad;

  private RingSpeedContext(bool isGrounded, float? groundAngleRad)
  {
    IsGrounded = isGrounded;
    GroundAngleRad = groundAngleRad;
  }

  public static RingSpeedContext GetGrounded(float groundAngleRad)
  {
    return new(true, groundAngleRad);
  }

  public static RingSpeedContext GetAirborn()
  {
    return new(false, null);
  }
}
