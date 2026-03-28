public readonly struct PlayerSpeedContext
{
  public readonly bool IsGrounded;
  public readonly bool PrevIsGrounded;
  public readonly float? GroundAngleRad;
  public readonly float? DistanceToGround;

  private PlayerSpeedContext(bool isGrounded, bool prevIsGrounded, float? groundAngleRad, float? distanceToGround)
  {
    IsGrounded = isGrounded;
    PrevIsGrounded = prevIsGrounded;
    GroundAngleRad = groundAngleRad;
    DistanceToGround = distanceToGround;
  }

  public static PlayerSpeedContext GetGrounded(bool prevIsGrounded, float groundAngleRad, float distanceToGround)
  {
    return new(true, prevIsGrounded, groundAngleRad, distanceToGround);
  }

  public static PlayerSpeedContext GetAirborne(bool prevIsGrounded)
  {
    return new(false, prevIsGrounded, null, null);
  }
}
