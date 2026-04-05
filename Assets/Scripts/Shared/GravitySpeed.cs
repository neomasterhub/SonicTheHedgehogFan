public readonly struct GravitySpeed
{
  public static readonly GravitySpeed Zero = new(0, 0);

  public readonly float Up;
  public readonly float Down;

  public GravitySpeed(float up, float down)
  {
    Up = up;
    Down = down;
  }
}
