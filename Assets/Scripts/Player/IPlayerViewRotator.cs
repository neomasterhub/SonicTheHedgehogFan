using UnityEngine;

public interface IPlayerViewRotator
{
  bool Enabled { get; set; }
  Vector3 Rotation { get; }
  void Rotate(PlayerViewRotatorInput input);
}
