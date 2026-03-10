public readonly struct PlayerViewInput
{
  public readonly bool IsSkidding;
  public readonly float TopSpeed;
  public readonly float MinAnimatorWalkingSpeed;
  public readonly float AnimatorWalkingSpeedFactor;
  public readonly GroundSide GroundSide;

  public PlayerViewInput(bool isSkidding, float topSpeed, float minAnimatorWalkingSpeed, float animatorWalkingSpeedFactor, GroundSide groundSide)
  {
    IsSkidding = isSkidding;
    TopSpeed = topSpeed;
    MinAnimatorWalkingSpeed = minAnimatorWalkingSpeed;
    AnimatorWalkingSpeedFactor = animatorWalkingSpeedFactor;
    GroundSide = groundSide;
  }
}
