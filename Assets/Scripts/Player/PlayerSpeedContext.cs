public class PlayerSpeedContext
{
  public bool IsGrounded { get; private set; }
  public bool PrevIsGrounded { get; private set; }
  public float? GroundAngleRad { get; private set; }
  public float? DistanceToGround { get; private set; }

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
