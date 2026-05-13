using UnityEngine;

public readonly struct SonicSpeedContext
{
  public readonly bool IsHit;
  public readonly bool IsRolling;
  public readonly bool IsJumping;
  public readonly bool IsGrounded;
  public readonly bool PrevIsGrounded;
  public readonly float? GroundAngleRad;
  public readonly float? DistanceToGround;
  public readonly float? DistanceToLeftWall;
  public readonly float? DistanceToRightWall;
  public readonly Vector2? HitSourcePosition;

  private SonicSpeedContext(bool isHit, Vector2? hitSourcePosition, bool isRolling, bool isJumping, bool isGrounded, bool prevIsGrounded, float? groundAngleRad, float? distanceToGround, float? distanceToLeftWall, float? distanceToRightWall)
  {
    IsHit = isHit;
    IsRolling = isRolling;
    IsJumping = isJumping;
    IsGrounded = isGrounded;
    PrevIsGrounded = prevIsGrounded;
    GroundAngleRad = groundAngleRad;
    DistanceToGround = distanceToGround;
    DistanceToLeftWall = distanceToLeftWall;
    DistanceToRightWall = distanceToRightWall;
    HitSourcePosition = hitSourcePosition;
  }

  public static SonicSpeedContext GetGrounded(bool isHit, Vector2? hitSourcePosition, bool isRolling, bool isJumping, bool prevIsGrounded, float groundAngleRad, float distanceToGround, float? distanceToLeftWall, float? distanceToRightWall)
  {
    return new(isHit, hitSourcePosition, isRolling, isJumping, true, prevIsGrounded, groundAngleRad, distanceToGround, distanceToLeftWall, distanceToRightWall);
  }

  public static SonicSpeedContext GetAirborne(bool isHit, Vector2? hitSourcePosition, bool isRolling, bool isJumping, bool prevIsGrounded, float? distanceToLeftWall, float? distanceToRightWall)
  {
    return new(isHit, hitSourcePosition, isRolling, isJumping, false, prevIsGrounded, null, null, distanceToLeftWall, distanceToRightWall);
  }
}
