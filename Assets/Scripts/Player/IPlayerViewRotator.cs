using System;
using UnityEngine;

public interface IPlayerViewRotator
{
  Vector3 Rotation { get; }
  Func<bool> Condition { get; }
  string DisplayName { get; }
  void Rotate(PlayerViewRotatorInput input);
}
