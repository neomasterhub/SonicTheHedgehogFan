public readonly struct SonicSpeedContext
{
  public readonly bool IsHit;
  public readonly float HitHorizontalDirection;
  public readonly bool IsDying;
  public readonly bool IsRolling;
  public readonly bool IsJumping;
  public readonly bool IsGrounded;
  public readonly bool PrevIsGrounded;
  public readonly float? GroundAngleRad;
  public readonly float? DistanceToGround;
  public readonly float? DistanceToLeftWall;
  public readonly float? DistanceToRightWall;
  public readonly float? CeilingAngleDeg;
  public readonly float? DistanceToCeiling;
  public readonly float? PushingSpeed;
  public readonly float? HDistanceToBlock;

  private SonicSpeedContext(bool isHit, float hitHorizontalDirection, bool isDying, bool isRolling, bool isJumping, bool isGrounded, bool prevIsGrounded, float? groundAngleRad, float? distanceToGround, float? distanceToLeftWall, float? distanceToRightWall, float? ceilingAngleDeg, float? distanceToCeiling, float? pushingSpeed, float? hDistanceToBlock)
  {
    IsHit = isHit;
    HitHorizontalDirection = hitHorizontalDirection;
    IsDying = isDying;
    IsRolling = isRolling;
    IsJumping = isJumping;
    IsGrounded = isGrounded;
    PrevIsGrounded = prevIsGrounded;
    GroundAngleRad = groundAngleRad;
    DistanceToGround = distanceToGround;
    DistanceToLeftWall = distanceToLeftWall;
    DistanceToRightWall = distanceToRightWall;
    CeilingAngleDeg = ceilingAngleDeg;
    DistanceToCeiling = distanceToCeiling;
    PushingSpeed = pushingSpeed;
    HDistanceToBlock = hDistanceToBlock;
  }

  public static SonicSpeedContext GetGrounded(bool isHit, float hitHorizontalDirection, bool isDying, bool isRolling, bool isJumping, bool prevIsGrounded, float groundAngleRad, float distanceToGround, float? distanceToLeftWall, float? distanceToRightWall, float? pushingSpeed, float? hDistanceToBlock)
  {
    return new(isHit, hitHorizontalDirection, isDying, isRolling, isJumping, true, prevIsGrounded, groundAngleRad, distanceToGround, distanceToLeftWall, distanceToRightWall, null, null, pushingSpeed, hDistanceToBlock);
  }

  public static SonicSpeedContext GetAirborne(bool isHit, float hitHorizontalDirection, bool isDying, bool isRolling, bool isJumping, bool prevIsGrounded, float? distanceToLeftWall, float? distanceToRightWall, float? ceilingAngleDeg, float? distanceToCeiling, float? pushingSpeed, float? hDistanceToBlock)
  {
    return new(isHit, hitHorizontalDirection, isDying, isRolling, isJumping, false, prevIsGrounded, null, null, distanceToLeftWall, distanceToRightWall, ceilingAngleDeg, distanceToCeiling, pushingSpeed, hDistanceToBlock);
  }
}
