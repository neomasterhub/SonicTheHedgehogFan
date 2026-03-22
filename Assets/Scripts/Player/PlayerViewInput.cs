public readonly struct PlayerViewInput
{
  public readonly float TopSpeed;
  public readonly float AnimatorParameterSpeedAirborneMin;
  public readonly float AnimatorSpeedWalkingMin;
  public readonly float AnimatorSpeedWalkingFactor;
  public readonly float StandingStraightGroundSpeedZone;
  public readonly float GroundSideAngleDeg;
  public readonly float GroundAngleDeg;
  public readonly GroundSide PrevGroundSide;
  public readonly PlayerState PlayerState;
  public readonly PlayerState PrevPlayerState;

  public PlayerViewInput(float topSpeed, float animatorParameterSpeedAirborneMin, float animatorSpeedWalkingMin, float animatorSpeedWalkingFactor, float standingStraightGroundSpeedZone, float groundSideAngleDeg, float groundAngleDeg, GroundSide prevGroundSide, PlayerState playerState, PlayerState prevPlayerState)
  {
    TopSpeed = topSpeed;
    AnimatorParameterSpeedAirborneMin = animatorParameterSpeedAirborneMin;
    AnimatorSpeedWalkingMin = animatorSpeedWalkingMin;
    AnimatorSpeedWalkingFactor = animatorSpeedWalkingFactor;
    StandingStraightGroundSpeedZone = standingStraightGroundSpeedZone;
    GroundSideAngleDeg = groundSideAngleDeg;
    GroundAngleDeg = groundAngleDeg;
    PrevGroundSide = prevGroundSide;
    PlayerState = playerState;
    PrevPlayerState = prevPlayerState;
  }
}
