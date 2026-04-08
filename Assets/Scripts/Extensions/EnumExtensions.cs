public static class EnumExtensions
{
  public static ButtonInput SetFlag(this ButtonInput source, ButtonInput target, bool enabled)
  {
    return enabled ? source | target : source & ~target;
  }

  public static SonicState SetFlag(this SonicState source, SonicState target, bool enabled)
  {
    return enabled ? source | target : source & ~target;
  }
}
