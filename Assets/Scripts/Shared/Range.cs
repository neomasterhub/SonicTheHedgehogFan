public class Range
{
  public Range()
  {
  }

  public Range(float min, float max)
  {
    Min = min;
    Max = max;
  }

  public float Min { get; set; }
  public float Max { get; set; }

  public bool Has(float value)
  {
    return Min <= value && value <= Max;
  }
}
