public static class EnumExtensions
{
  public static PlayerInput SetFlag(this PlayerInput source, PlayerInput target, bool enabled)
  {
    return enabled ? source | target : source & ~target;
  }

  public static SonicState SetFlag(this SonicState source, SonicState target, bool enabled)
  {
    return enabled ? source | target : source & ~target;
  }
}
