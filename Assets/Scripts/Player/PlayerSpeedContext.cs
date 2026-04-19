public readonly struct PlayerSpeedContext
{
  public readonly bool IsGrounded;
  public readonly bool PrevIsGrounded;
  public readonly float? GroundAngleRad;
  public readonly float? DistanceToGround;
  public readonly float? DistanceToLeftWall;
  public readonly float? DistanceToRightWall;

  private PlayerSpeedContext(bool isGrounded, bool prevIsGrounded, float? groundAngleRad, float? distanceToGround, float? distanceToLeftWall, float? distanceToRightWall)
  {
    IsGrounded = isGrounded;
    PrevIsGrounded = prevIsGrounded;
    GroundAngleRad = groundAngleRad;
    DistanceToGround = distanceToGround;
    DistanceToLeftWall = distanceToLeftWall;
    DistanceToRightWall = distanceToRightWall;
  }

  public static PlayerSpeedContext GetGrounded(bool prevIsGrounded, float groundAngleRad, float distanceToGround, float? distanceToLeftWall, float? distanceToRightWall)
  {
    return new(true, prevIsGrounded, groundAngleRad, distanceToGround, distanceToLeftWall, distanceToRightWall);
  }

  public static PlayerSpeedContext GetAirborne(bool prevIsGrounded, float? distanceToLeftWall, float? distanceToRightWall)
  {
    return new(false, prevIsGrounded, null, null, distanceToLeftWall, distanceToRightWall);
  }
}
