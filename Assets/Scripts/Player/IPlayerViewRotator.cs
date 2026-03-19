using UnityEngine;

public interface IPlayerViewRotator
{
  bool Enabled { get; set; }
  Quaternion Rotation { get; }
  void Rotate(PlayerViewRotatorInput input);
}
