public readonly struct SonicViewContext
{
  public readonly float SpeedX;
  public readonly SonicState State;
  public readonly SonicState PrevState;
  public readonly GroundSide PrevGroundSide;

  public SonicViewContext(float speedX, SonicState state, SonicState prevState, GroundSide prevGroundSide)
  {
    SpeedX = speedX;
    State = state;
    PrevState = prevState;
    PrevGroundSide = prevGroundSide;
  }
}
