using UnityEngine;

public abstract class PlayerViewRotatorBase : IPlayerViewRotator
{
  public bool Enabled { get; set; } = true;
  public Vector3 Rotation { get; protected set; }
  public abstract void Rotate(PlayerViewRotatorInput input);
}
