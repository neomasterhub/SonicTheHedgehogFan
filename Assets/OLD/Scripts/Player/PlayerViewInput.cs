public readonly struct PlayerViewInput
{
  public readonly float AnimatorParameterSpeedAirborneMin;
  public readonly float AnimatorSpeedWalkingMin;
  public readonly float AnimatorSpeedWalkingFactor;
  public readonly float SpeedX;
  public readonly float TopSpeed;
  public readonly float GroundSpeed;
  public readonly float GroundAngleDeg;
  public readonly float GroundSideAngleDeg;
  public readonly float StandingStraightGroundSpeedZone;
  public readonly GroundSide PrevGroundSide;
  public readonly PlayerState PlayerState;
  public readonly PlayerState PrevPlayerState;
  public readonly SensorId? GroundSensorIdApplied;

  public PlayerViewInput(float animatorParameterSpeedAirborneMin, float animatorSpeedWalkingMin, float animatorSpeedWalkingFactor, float speedX, float topSpeed, float groundSpeed, float groundAngleDeg, float groundSideAngleDeg, float standingStraightGroundSpeedZone, GroundSide prevGroundSide, PlayerState playerState, PlayerState prevPlayerState, SensorId? groundSensorIdApplied)
  {
    AnimatorParameterSpeedAirborneMin = animatorParameterSpeedAirborneMin;
    AnimatorSpeedWalkingMin = animatorSpeedWalkingMin;
    AnimatorSpeedWalkingFactor = animatorSpeedWalkingFactor;
    SpeedX = speedX;
    TopSpeed = topSpeed;
    GroundSpeed = groundSpeed;
    GroundAngleDeg = groundAngleDeg;
    GroundSideAngleDeg = groundSideAngleDeg;
    StandingStraightGroundSpeedZone = standingStraightGroundSpeedZone;
    PrevGroundSide = prevGroundSide;
    PlayerState = playerState;
    PrevPlayerState = prevPlayerState;
    GroundSensorIdApplied = groundSensorIdApplied;
  }
}
