public static class GroundSideExtensions
{
  public static GroundSide GetPrevious(this GroundSide side)
  {
    return side switch
    {
      GroundSide.Down => GroundSide.Left,
      GroundSide.Left => GroundSide.Up,
      GroundSide.Up => GroundSide.Right,
      GroundSide.Right => GroundSide.Down,
      _ => throw side.ArgumentOutOfRangeException(),
    };
  }

  public static GroundSide GetNext(this GroundSide side)
  {
    return side switch
    {
      GroundSide.Down => GroundSide.Right,
      GroundSide.Right => GroundSide.Up,
      GroundSide.Up => GroundSide.Left,
      GroundSide.Left => GroundSide.Down,
      _ => throw side.ArgumentOutOfRangeException(),
    };
  }
}
