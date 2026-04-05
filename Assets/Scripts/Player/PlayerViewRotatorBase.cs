using System;
using UnityEngine;

public abstract class PlayerViewRotatorBase<TContext>
  : IPlayerViewRotator<TContext>
  where TContext : struct
{
  protected PlayerViewRotatorBase(string displayName, Func<bool> condition)
  {
    DisplayName = displayName;
    Condition = condition;
  }

  public string DisplayName { get; }
  public Func<bool> Condition { get; }
  public Vector3 Rotation { get; protected set; }
  public abstract void Rotate(TContext context);

  public override string ToString()
  {
    return DisplayName;
  }
}
