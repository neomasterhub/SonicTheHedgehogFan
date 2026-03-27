public static class Extensions
{
  public static float GetAngle(this GroundSide side, float relativeSideAngle = 0)
  {
    return relativeSideAngle + side switch
    {
      GroundSide.Down => 0,
      GroundSide.Right => 90,
      GroundSide.Up => 180,
      GroundSide.Left => -90,
      _ => throw side.ArgumentOutOfRangeException()
    };
  }
}
