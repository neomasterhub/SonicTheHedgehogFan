using UnityEngine;

public abstract class PlayerViewRotatorBase : IPlayerViewRotator
{
  public bool Enabled { get; set; }
  public Quaternion Rotation { get; protected set; }
  public abstract void Rotate(PlayerViewRotatorInput input);
}
