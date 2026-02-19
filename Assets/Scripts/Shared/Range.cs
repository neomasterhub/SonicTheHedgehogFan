public class Range
{
  public float Min { get; set; }
  public float Max { get; set; }

  public bool Has(float value)
  {
    return Min <= value && value <= Max;
  }
}
