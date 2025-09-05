public static class SonicConsts
{
  public enum Pose
  {
    Crouching,
    Standing,
  }

  public static class AnimatorParameterNames
  {
    public const string Speed = nameof(Speed);
  }

  public static class AnimatorStateNames
  {
    public const string Idle = nameof(Idle);
    public const string Bored = nameof(Bored);
    public const string Waiting = nameof(Waiting);
    public const string Walking = nameof(Walking);
  }
}
