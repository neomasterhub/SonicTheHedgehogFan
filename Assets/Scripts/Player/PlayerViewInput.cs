public readonly struct PlayerViewInput
{
  public readonly bool IsSkidding;
  public readonly float TopSpeed;
  public readonly float MinAnimatorWalkingSpeed;
  public readonly float AnimatorWalkingSpeedFactor;
  public readonly float GroundSideAngleDeg;
  public readonly float GroundAngleDeg;
  public readonly GroundSide PrevGroundSide;
  public readonly PlayerState PlayerState;
  public readonly PlayerState PrevPlayerState;

  public PlayerViewInput(bool isSkidding, float topSpeed, float minAnimatorWalkingSpeed, float animatorWalkingSpeedFactor, float groundSideAngleDeg, float groundAngleDeg, GroundSide prevGroundSide, PlayerState playerState, PlayerState prevPlayerState)
  {
    IsSkidding = isSkidding;
    TopSpeed = topSpeed;
    MinAnimatorWalkingSpeed = minAnimatorWalkingSpeed;
    AnimatorWalkingSpeedFactor = animatorWalkingSpeedFactor;
    GroundSideAngleDeg = groundSideAngleDeg;
    GroundAngleDeg = groundAngleDeg;
    PrevGroundSide = prevGroundSide;
    PlayerState = playerState;
    PrevPlayerState = prevPlayerState;
  }
}
