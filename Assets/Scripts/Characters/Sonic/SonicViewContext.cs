public readonly struct SonicViewContext
{
  public readonly bool HorizontalDirection;
  public readonly bool IsHurt;
  public readonly bool IsGrounded;
  public readonly bool IsSkidding;
  public readonly bool IsBalancing;
  public readonly bool IsCurlingUp;
  public readonly bool IsLookingUp;
  public readonly bool IsRolling;
  public readonly bool IsZeroGroundSpeedProgressReached;
  public readonly bool? TriggeredGroundSensorSide;
  public readonly float SpeedX;
  public readonly float GroundSpeed;
  public readonly float GroundAngleDeg;
  public readonly float DeltaTime;
  public readonly GroundSide GroundSide;
  public readonly GroundSide PrevGroundSide;

  public SonicViewContext(bool horizontalDirection, bool isHurt, bool isGrounded, bool isSkidding, bool isBalancing, bool isCurlingUp, bool isLookingUp, bool isRolling, bool isZeroGroundSpeedProgressReached, bool? triggeredGroundSensorSide, float speedX, float groundSpeed, float groundAngleDeg, float deltaTime, GroundSide groundSide, GroundSide prevGroundSide)
  {
    HorizontalDirection = horizontalDirection;
    IsHurt = isHurt;
    IsGrounded = isGrounded;
    IsSkidding = isSkidding;
    IsBalancing = isBalancing;
    IsCurlingUp = isCurlingUp;
    IsLookingUp = isLookingUp;
    IsRolling = isRolling;
    IsZeroGroundSpeedProgressReached = isZeroGroundSpeedProgressReached;
    TriggeredGroundSensorSide = triggeredGroundSensorSide;
    SpeedX = speedX;
    GroundSpeed = groundSpeed;
    GroundAngleDeg = groundAngleDeg;
    DeltaTime = deltaTime;
    GroundSide = groundSide;
    PrevGroundSide = prevGroundSide;
  }
}
