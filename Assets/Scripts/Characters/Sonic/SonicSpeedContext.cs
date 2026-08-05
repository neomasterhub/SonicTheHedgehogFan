using UnityEngine;

public readonly struct SonicSpeedContext
{
  public readonly bool IsHit;
  public readonly bool HorizontalDirection;
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
  public readonly IBlock ContactBlock;
  public readonly float? ReboundGroundSpeed;
  public readonly Vector2? ReboundAirSpeed;
  public readonly bool IsStoppedByCeiling;
  public readonly bool IsSpinDashReleased;

  private SonicSpeedContext(bool isHit, bool horizontalDirection, float hitHorizontalDirection, bool isDying, bool isRolling, bool isJumping, bool isGrounded, bool prevIsGrounded, float? groundAngleRad, float? distanceToGround, float? distanceToLeftWall, float? distanceToRightWall, float? ceilingAngleDeg, float? distanceToCeiling, IBlock contactBlock, float? reboundGroundSpeed, Vector2? reboundAirSpeed, bool isStoppedByCeiling, bool isSpinDashReleased)
  {
    IsHit = isHit;
    HorizontalDirection = horizontalDirection;
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
    ContactBlock = contactBlock;
    ReboundGroundSpeed = reboundGroundSpeed;
    ReboundAirSpeed = reboundAirSpeed;
    IsStoppedByCeiling = isStoppedByCeiling;
    IsSpinDashReleased = isSpinDashReleased;
  }

  public static SonicSpeedContext GetGrounded(bool isHit, bool horizontalDirection, float hitHorizontalDirection, bool isDying, bool isRolling, bool isJumping, bool prevIsGrounded, float groundAngleRad, float distanceToGround, float? distanceToLeftWall, float? distanceToRightWall, IBlock contactBlock, float? reboundGroundSpeed, bool isStoppedByCeiling, bool isSpinDashReleased)
  {
    return new(isHit, horizontalDirection, hitHorizontalDirection, isDying, isRolling, isJumping, true, prevIsGrounded, groundAngleRad, distanceToGround, distanceToLeftWall, distanceToRightWall, null, null, contactBlock, reboundGroundSpeed, null, isStoppedByCeiling, isSpinDashReleased);
  }

  public static SonicSpeedContext GetAirborne(bool isHit, bool horizontalDirection, float hitHorizontalDirection, bool isDying, bool isRolling, bool isJumping, bool prevIsGrounded, float? distanceToLeftWall, float? distanceToRightWall, float? ceilingAngleDeg, float? distanceToCeiling, Vector2? reboundAirSpeed, bool isStoppedByCeiling)
  {
    return new(isHit, horizontalDirection, hitHorizontalDirection, isDying, isRolling, isJumping, false, prevIsGrounded, null, null, distanceToLeftWall, distanceToRightWall, ceilingAngleDeg, distanceToCeiling, null, null, reboundAirSpeed, isStoppedByCeiling, false);
  }
}
