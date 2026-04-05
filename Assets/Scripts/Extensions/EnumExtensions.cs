public static class EnumExtensions
{
  public static SonicState SetFlag(
    this SonicState source,
    SonicState target,
    bool enabled)
  {
    return enabled
      ? source | target
      : source & ~target;
  }
}
