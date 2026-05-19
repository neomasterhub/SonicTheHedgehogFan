public readonly struct GroundEnemyAIContext
{
  public readonly bool IsGrounded;
  public readonly bool PrevIsGrounded;
  public readonly float? GroundAngleRad;

  public GroundEnemyAIContext(bool isGrounded, bool prevIsGrounded, float? groundAngleRad)
  {
    IsGrounded = isGrounded;
    PrevIsGrounded = prevIsGrounded;
    GroundAngleRad = groundAngleRad;
  }
}
