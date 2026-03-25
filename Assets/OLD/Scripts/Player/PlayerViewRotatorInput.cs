public readonly struct PlayerViewRotatorInput
{
  public readonly float GroundAngleDeg;
  public readonly float GroundSpeed;
  public readonly float StandingStraightGroundSpeedZone;
  public readonly GroundSide PrevGroundSide;
  public readonly PlayerState PlayerState;
  public readonly PlayerState PrevPlayerState;

  public PlayerViewRotatorInput(float groundAngleDeg, float groundSpeed, float standingStraightGroundSpeedZone, GroundSide prevGroundSide, PlayerState playerState, PlayerState prevPlayerState)
  {
    GroundAngleDeg = groundAngleDeg;
    GroundSpeed = groundSpeed;
    StandingStraightGroundSpeedZone = standingStraightGroundSpeedZone;
    PrevGroundSide = prevGroundSide;
    PlayerState = playerState;
    PrevPlayerState = prevPlayerState;
  }
}
