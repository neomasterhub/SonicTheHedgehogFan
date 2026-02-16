public static class Consts
{
  public static class ConvertValues
  {
    public const int FramePerSec = 60;
    public const int PxPerUnit = 40;
    public const int SpxPerPx = 256;
    public const int SpxPerUnit = SpxPerPx * PxPerUnit;
  }

  public static class InputAxis
  {
    public const string Jump = nameof(Jump);
    public const string Horizontal = nameof(Horizontal);
    public const string Vertical = nameof(Vertical);
  }

  public static class Animator
  {
    public static class Parameters
    {
      public const string Speed = nameof(Speed);
      public const string IsSkidding = nameof(IsSkidding);
    }

    public static class States
    {
      public const string Idle = nameof(Idle);
      public const string Bored = nameof(Bored);
      public const string Waiting = nameof(Waiting);
      public const string Walking = nameof(Walking);
      public const string Running = nameof(Running);
    }
  }
}
