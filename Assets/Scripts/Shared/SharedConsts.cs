using UnityEngine;
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

  public static class UI
  {
    public const float DebugScale = 10000;
    public const int PlayerOrderInLayer = 1;
  }

  public static class Input
  {
    public const int PressedHistoryCapacity = 10;
  }

  public static class Physics
  {
    public const int SpeedRoundingDigits = 3;
    public const float PositionBackwardOffset = 0.02f;
    public const float GroundedPositionUpwardOffset = 0.05f;
    public static readonly LayerMask GroundLayer = 1 << 3;

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
      public const string LookingUp = nameof(LookingUp);
      public const string Rolling = nameof(Rolling);
      public const string Collected = nameof(Collected);
      public const string Hurt = nameof(Hurt);
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
      public const string LookingUp = nameof(LookingUp);
      public const string Rolling = nameof(Rolling);
      public const string Hurt = nameof(Hurt);
    }
  }

  public static class SecretCodes
  {
    public static readonly PlayerInput[] TakeLeftHit = new[] { B, X, Left };
    public static readonly PlayerInput[] TakeRightHit = new[] { B, X, Right };
    public static readonly PlayerInput[] ToggleDebugMode = new[] { Y };
  }
}
