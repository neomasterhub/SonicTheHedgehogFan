public static class EnumExtensions
{
  public static PlayerState SetFlag(
    this PlayerState source,
    PlayerState target,
    bool enabled)
  {
    return enabled
      ? source | target
      : source & ~target;
  }
}
