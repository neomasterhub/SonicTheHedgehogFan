using System;
using UnityEngine;

public abstract class PlayerViewRotatorBase : IPlayerViewRotator
{
  public Func<bool> Condition { get; }
  public Vector3 Rotation { get; protected set; }
  public abstract void Rotate(PlayerViewRotatorInput input);
}
