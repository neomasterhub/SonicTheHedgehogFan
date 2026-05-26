using UnityEngine;
using static PlayerInput;

public static class SharedConsts
{
  public static class Animator
  {
    public static class Parameters
    {
      public const string Balancing = nameof(Balancing);
      public const string Collected = nameof(Collected);
      public const string CurlingUp = nameof(CurlingUp);
      public const string Dead = nameof(Dead);
      public const string Dying = nameof(Dying);
      public const string Hit = nameof(Hit);
      public const string Hurt = nameof(Hurt);
      public const string Idle = nameof(Idle);
      public const string LookingUp = nameof(LookingUp);
      public const string Rolling = nameof(Rolling);
      public const string Skidding = nameof(Skidding);
      public const string Speed = nameof(Speed);
      public const string Static = nameof(Static);
    }

    public static class States
    {
      public const string Rolling = nameof(Rolling);
      public const string Walking = nameof(Walking);
    }
  }

  public static class Colors
  {
    public static readonly Color TransparentBlack = new(0, 0, 0, 0);
  }

  public static class ConvertValues
  {
    public const int FramePerSec = 120;
    public const int PxPerUnit = 40;
    public const int SpxPerPx = 256;
    public const int SpxPerUnit = SpxPerPx * PxPerUnit;
  }

  public static class Input
  {
    public const int PressedHistoryCapacity = 10;
  }

  public static class Physics
  {
    public const int SpeedRoundingDigits = 3;
    public const int PositionRoundingDigits = 3;
    public const float GroundedPositionUpwardOffset = 0.05f;
    public static readonly LayerMask GroundLayer = 1 << 3;

    public static class GroundAngleRanges
    {
      public static readonly Range Flat = new(-23, 23);
      public static readonly Range Slope = new(-45, 45);
    }
  }

  public static class Rendering
  {
    public const float DebugScale = 10000;
    public const int PlayerOrderInLayer = 1;
  }

  public static class SecretCodes
  {
    public static readonly PlayerInput[] TakeLeftHit = new[] { X, X, A };
    public static readonly PlayerInput[] TakeRightHit = new[] { X, X, B };
    public static readonly PlayerInput[] ToggleDebugMode = new[] { Z, Y, Y };
  }
}
