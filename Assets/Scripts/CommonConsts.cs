public static class CommonConsts
{
  public static class ConvertValues
  {
    public const int FramePerSec = 60;
    public const int PxPerUnit = 40;
    public const int SpxPerPx = 256;
    public const int SpxPerUnit = SpxPerPx * PxPerUnit;
  }

  public static class Physics
  {
    public const float GravityDownSpx = 56;
    public const float GravityUpSpx = 16;
    public const float MaxFallSpeedPx = 16;
    public const float GravityDown = GravityDownSpx / ConvertValues.SpxPerUnit;
    public const float GravityUp = GravityUpSpx / ConvertValues.SpxPerUnit;
    public const float MaxFallSpeed = MaxFallSpeedPx / ConvertValues.PxPerUnit;
  }

  public static class InputAxis
  {
    public const string Jump = nameof(Jump);
    public const string Horizontal = nameof(Horizontal);
    public const string Vertical = nameof(Vertical);
  }
}
