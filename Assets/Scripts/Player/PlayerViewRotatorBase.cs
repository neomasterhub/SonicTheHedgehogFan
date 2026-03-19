public abstract class PlayerViewRotatorBase : IPlayerViewRotator
{
  public bool Enabled { get; set; } = true;
  public float Angle { get; protected set; }
  public abstract void Rotate(PlayerViewRotatorInput input);
}
