public readonly struct SonicViewContext
{
  public readonly bool IsGrounded;
  public readonly bool IsSkidding;
  public readonly bool IsBalancing;
  public readonly float SpeedX;
  public readonly SonicState State;
  public readonly SonicState PrevState;
  public readonly GroundSide PrevGroundSide;
}
