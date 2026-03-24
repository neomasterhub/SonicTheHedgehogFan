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
  public readonly SensorId? GroundSensorIdApplied;
}
