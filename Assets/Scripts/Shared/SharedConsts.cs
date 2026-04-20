using static PlayerInput;

public static class SharedConsts
{
  public static class ConvertValues
  {
    public const int FramePerSec = 60;
    public const int PxPerUnit = 40;
    public const int SpxPerPx = 256;
    public const int SpxPerUnit = SpxPerPx * PxPerUnit;
  }

  public static class Input
  {
    public const int PressedHistoryCapacity = 10;
    public const float InputDeadZone = 0.001f;
  }

  public static class Physics
  {
    public const float PositionBackwardOffset = 0.01f;
    public const float GroundedPositionUpwardOffset = 0.05f;

    public static class GroundAngleRanges
    {
      public static readonly Range Flat = new(-23, 23);
      public static readonly Range Slope = new(-45, 45);
    }
  }

  public static class Animator
  {
    public static class Parameters
    {
      public const string Idle = nameof(Idle);
      public const string Speed = nameof(Speed);
      public const string Skidding = nameof(Skidding);
      public const string Balancing = nameof(Balancing);
      public const string CurlingUp = nameof(CurlingUp);
    }

    public static class States
    {
      public const string Idle = nameof(Idle);
      public const string Bored = nameof(Bored);
      public const string Waiting = nameof(Waiting);
      public const string Walking = nameof(Walking);
      public const string Running = nameof(Running);
      public const string Skidding = nameof(Skidding);
      public const string Balancing = nameof(Balancing);
      public const string CurlingUp = nameof(CurlingUp);
    }
  }

  public static class SecretCodes
  {
    public static readonly PlayerInput[] ToggleDebugMode = new PlayerInput[] { Y, Y, B };
  }
}
