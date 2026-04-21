public readonly struct SonicViewContext
{
  public readonly bool IsGrounded;
  public readonly bool IsSkidding;
  public readonly bool IsBalancing;
  public readonly bool IsCurlingUp;
  public readonly bool IsLookingUp;
  public readonly bool IsZeroGroundSpeedProgressReached;
  public readonly bool? TriggeredGroundSensorSide;
  public readonly float SpeedX;
  public readonly float GroundSpeed;
  public readonly float GroundAngleDeg;
  public readonly GroundSide GroundSide;
  public readonly GroundSide PrevGroundSide;

  public SonicViewContext(bool isGrounded, bool isSkidding, bool isBalancing, bool isCurlingUp, bool isLookingUp, bool isZeroGroundSpeedProgressReached, bool? triggeredGroundSensorSide, float speedX, float groundSpeed, float groundAngleDeg, GroundSide groundSide, GroundSide prevGroundSide)
  {
    IsGrounded = isGrounded;
    IsSkidding = isSkidding;
    IsBalancing = isBalancing;
    IsCurlingUp = isCurlingUp;
    IsLookingUp = isLookingUp;
    IsZeroGroundSpeedProgressReached = isZeroGroundSpeedProgressReached;
    TriggeredGroundSensorSide = triggeredGroundSensorSide;
    SpeedX = speedX;
    GroundSpeed = groundSpeed;
    GroundAngleDeg = groundAngleDeg;
    GroundSide = groundSide;
    PrevGroundSide = prevGroundSide;
  }
}
