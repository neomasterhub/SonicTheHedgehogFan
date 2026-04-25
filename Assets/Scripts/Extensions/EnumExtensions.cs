public static class EnumExtensions
{
  public static string ToShortString(this PipelineStepResult result)
  {
    return result switch
    {
      PipelineStepResult.Break => "BR",
      PipelineStepResult.Continue => "CN",
      _ => throw result.ArgumentOutOfRangeException(),
    };
  }

  public static char ToChar(this PlayerInput input)
  {
    return input switch
    {
      PlayerInput.None => ' ',
      PlayerInput.Start => '*',
      PlayerInput.Up => '↑',
      PlayerInput.Down => '↓',
      PlayerInput.Left => '←',
      PlayerInput.Right => '→',
      PlayerInput.A => 'A',
      PlayerInput.B => 'B',
      PlayerInput.C => 'C',
      PlayerInput.X => 'x',
      PlayerInput.Y => 'y',
      PlayerInput.Z => 'z',
      _ => '?'
    };
  }

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

  public static PlayerInput Set(this PlayerInput source, PlayerInput target, bool enabled)
  {
    return enabled ? source | target : source & ~target;
  }

  public static SonicState Set(this SonicState source, SonicState target, bool enabled)
  {
    return enabled ? source | target : source & ~target;
  }
}
