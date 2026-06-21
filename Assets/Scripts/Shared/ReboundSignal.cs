public readonly struct ReboundSignal
{
  public readonly ReboundSourceType SourceType;
  public readonly int SourceHealth;

  public ReboundSignal(ReboundSourceType sourceType, int sourceHealth)
  {
    SourceType = sourceType;
    SourceHealth = sourceHealth;
  }
}
