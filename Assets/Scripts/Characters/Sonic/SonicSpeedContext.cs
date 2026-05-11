public readonly struct SonicSpeedContext
{
  public readonly bool IsHurt;
  public readonly bool IsRolling;
  public readonly bool IsJumping;
  public readonly bool IsGrounded;
  public readonly bool PrevIsGrounded;
  public readonly float? GroundAngleRad;
  public readonly float? DistanceToGround;
  public readonly float? DistanceToLeftWall;
  public readonly float? DistanceToRightWall;

  private SonicSpeedContext(bool isHurt, bool isRolling, bool isJumping, bool isGrounded, bool prevIsGrounded, float? groundAngleRad, float? distanceToGround, float? distanceToLeftWall, float? distanceToRightWall)
  {
    IsHurt = isHurt;
    IsRolling = isRolling;
    IsJumping = isJumping;
    IsGrounded = isGrounded;
    PrevIsGrounded = prevIsGrounded;
    GroundAngleRad = groundAngleRad;
    DistanceToGround = distanceToGround;
    DistanceToLeftWall = distanceToLeftWall;
    DistanceToRightWall = distanceToRightWall;
  }

  public static SonicSpeedContext GetGrounded(bool isHurt, bool isRolling, bool isJumping, bool prevIsGrounded, float groundAngleRad, float distanceToGround, float? distanceToLeftWall, float? distanceToRightWall)
  {
    return new(isHurt, isRolling, isJumping, true, prevIsGrounded, groundAngleRad, distanceToGround, distanceToLeftWall, distanceToRightWall);
  }

  public static SonicSpeedContext GetAirborne(bool isHurt, bool isRolling, bool isJumping, bool prevIsGrounded, float? distanceToLeftWall, float? distanceToRightWall)
  {
    return new(isHurt, isRolling, isJumping, false, prevIsGrounded, null, null, distanceToLeftWall, distanceToRightWall);
  }
}
