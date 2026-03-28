public readonly struct PlayerSpeedContext
{
  public readonly bool IsGrounded;
  public readonly bool PrevIsGrounded;
  public readonly float? GroundAngleRad;
  public readonly float? DistanceToGround;

  public void SetGrounded(bool prevIsGrounded, float groundAngleRad, float distanceToGround)
  {
    IsGrounded = true;
    PrevIsGrounded = prevIsGrounded;
    GroundAngleRad = groundAngleRad;
    DistanceToGround = distanceToGround;
  }

  public void SetAirborne(bool prevIsGrounded)
  {
    IsGrounded = false;
    PrevIsGrounded = prevIsGrounded;
    GroundAngleRad = null;
    DistanceToGround = null;
  }
}
