public readonly struct SonicSensorChecks
{
  public readonly bool Ground;
  public readonly bool Ceiling;
  public readonly bool Balancing;

  public SonicSensorChecks(bool ground, bool ceiling, bool balancing)
  {
    Ground = ground;
    Ceiling = ceiling;
    Balancing = balancing;
  }
}
