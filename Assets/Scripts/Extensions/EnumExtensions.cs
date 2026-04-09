public static class EnumExtensions
{
  public static bool HasAll(this PlayerInput source, PlayerInput target)
  {
    return (source & target) == target;
  }

  public static bool HasAll(this SonicState source, SonicState target)
  {
    return (source & target) == target;
  }

  public static bool HasAny(this PlayerInput source, PlayerInput target)
  {
    return (source & target) != 0;
  }

  public static bool HasAny(this SonicState source, SonicState target)
  {
    return (source & target) != 0;
  }

  public static PlayerInput SetFlag(this PlayerInput source, PlayerInput target, bool enabled)
  {
    return enabled ? source | target : source & ~target;
  }

  public static SonicState SetFlag(this SonicState source, SonicState target, bool enabled)
  {
    return enabled ? source | target : source & ~target;
  }
}
