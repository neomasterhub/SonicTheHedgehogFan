public readonly struct ReboundSignal
{
  public readonly ReboundSourceType SourceType;
  public readonly int SourceHealth;
  public readonly float SourceSpeedX;
  public readonly float SourceSpeedY;

  public ReboundSignal(ReboundSourceType sourceType, int sourceHealth, float sourceSpeedX, float sourceSpeedY)
  {
    SourceType = sourceType;
    SourceHealth = sourceHealth;
    SourceSpeedX = sourceSpeedX;
    SourceSpeedY = sourceSpeedY;
  }
}
