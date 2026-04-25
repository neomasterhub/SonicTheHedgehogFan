public readonly struct SonicSpeedContext
{
  public readonly bool IsRolling;
  public readonly bool IsGrounded;
  public readonly bool PrevIsGrounded;
  public readonly float? GroundAngleRad;
  public readonly float? DistanceToGround;
  public readonly float? DistanceToLeftWall;
  public readonly float? DistanceToRightWall;

  private SonicSpeedContext(bool isRolling, bool isGrounded, bool prevIsGrounded, float? groundAngleRad, float? distanceToGround, float? distanceToLeftWall, float? distanceToRightWall)
  {
    IsRolling = isRolling;
    IsGrounded = isGrounded;
    PrevIsGrounded = prevIsGrounded;
    GroundAngleRad = groundAngleRad;
    DistanceToGround = distanceToGround;
    DistanceToLeftWall = distanceToLeftWall;
    DistanceToRightWall = distanceToRightWall;
  }

  public static SonicSpeedContext GetGrounded(bool isRolling, bool prevIsGrounded, float groundAngleRad, float distanceToGround, float? distanceToLeftWall, float? distanceToRightWall)
  {
    return new(isRolling, true, prevIsGrounded, groundAngleRad, distanceToGround, distanceToLeftWall, distanceToRightWall);
  }

  public static SonicSpeedContext GetAirborne(bool isRolling, bool prevIsGrounded, float? distanceToLeftWall, float? distanceToRightWall)
  {
    return new(isRolling, false, prevIsGrounded, null, null, distanceToLeftWall, distanceToRightWall);
  }
}
