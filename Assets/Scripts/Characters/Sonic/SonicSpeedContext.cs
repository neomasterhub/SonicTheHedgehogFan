public readonly struct SonicSpeedContext
{
  public readonly bool IsHit;
  public readonly float HitHorizontalDirection;
  public readonly bool IsRolling;
  public readonly bool IsJumping;
  public readonly bool IsGrounded;
  public readonly bool PrevIsGrounded;
  public readonly float? GroundAngleRad;
  public readonly float? DistanceToGround;
  public readonly float? DistanceToLeftWall;
  public readonly float? DistanceToRightWall;

  private SonicSpeedContext(bool isHit, float hitHorizontalDirection, bool isRolling, bool isJumping, bool isGrounded, bool prevIsGrounded, float? groundAngleRad, float? distanceToGround, float? distanceToLeftWall, float? distanceToRightWall)
  {
    IsHit = isHit;
    HitHorizontalDirection = hitHorizontalDirection;
    IsRolling = isRolling;
    IsJumping = isJumping;
    IsGrounded = isGrounded;
    PrevIsGrounded = prevIsGrounded;
    GroundAngleRad = groundAngleRad;
    DistanceToGround = distanceToGround;
    DistanceToLeftWall = distanceToLeftWall;
    DistanceToRightWall = distanceToRightWall;
  }

  public static SonicSpeedContext GetGrounded(bool isHit, float hitHorizontalDirection, bool isRolling, bool isJumping, bool prevIsGrounded, float groundAngleRad, float distanceToGround, float? distanceToLeftWall, float? distanceToRightWall)
  {
    return new(isHit, hitHorizontalDirection, isRolling, isJumping, true, prevIsGrounded, groundAngleRad, distanceToGround, distanceToLeftWall, distanceToRightWall);
  }

  public static SonicSpeedContext GetAirborne(bool isHit, float hitHorizontalDirection, bool isRolling, bool isJumping, bool prevIsGrounded, float? distanceToLeftWall, float? distanceToRightWall)
  {
    return new(isHit, hitHorizontalDirection, isRolling, isJumping, false, prevIsGrounded, null, null, distanceToLeftWall, distanceToRightWall);
  }
}
