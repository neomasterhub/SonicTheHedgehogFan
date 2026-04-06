public static class GroundSideExtensions
{
  public static char GetFirstChar(this GroundSide side)
  {
    return side switch
    {
      GroundSide.Down => 'D',
      GroundSide.Right => 'R',
      GroundSide.Up => 'U',
      GroundSide.Left => 'L',
      _ => throw side.ArgumentOutOfRangeException()
    };
  }

  public static float GetCcwAngleDeg(this GroundSide side)
  {
    return side switch
    {
      GroundSide.Down => 0,
      GroundSide.Right => 90,
      GroundSide.Up => 180,
      GroundSide.Left => 270,
      _ => throw side.ArgumentOutOfRangeException()
    };
  }

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
