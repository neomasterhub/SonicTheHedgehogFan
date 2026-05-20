public readonly struct GroundEnemyAIContext
{
  public readonly bool IsGrounded;
  public readonly float? GroundAngleRad;

  public GroundEnemyAIContext(bool isGrounded, float? groundAngleRad)
  {
    IsGrounded = isGrounded;
    GroundAngleRad = groundAngleRad;
  }
}
