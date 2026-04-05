using System;
using UnityEngine;

public interface IPlayerViewRotator<TContext>
  where TContext : struct
{
  string DisplayName { get; }
  Vector3 Rotation { get; }
  Func<bool> Condition { get; }
  void Rotate(TContext context);
}
