public readonly struct PlayerViewRotatorInput
{
  public readonly float GroundAngleDeg;
  public readonly float GroundSpeed;
  public readonly float StandingStraightGroundSpeedZone;
  public readonly PlayerState PlayerState;

  public PlayerViewRotatorInput(float groundAngleDeg, float groundSpeed, float standingStraightGroundSpeedZone, PlayerState playerState)
  {
    GroundAngleDeg = groundAngleDeg;
    GroundSpeed = groundSpeed;
    StandingStraightGroundSpeedZone = standingStraightGroundSpeedZone;
    PlayerState = playerState;
  }
}
