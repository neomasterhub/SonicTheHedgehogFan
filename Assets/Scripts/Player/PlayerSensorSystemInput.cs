using UnityEngine;

public readonly struct PlayerSensorSystemInput
{
  public readonly LayerMask GroundLayer;
  public readonly float ABSensorLength;
  public readonly float ReversedABSensorLength;
  public readonly bool HorizontalDirection;

  public PlayerSensorSystemInput(
    LayerMask groundLayer,
    float aBSensorLength,
    float reversedABSensorLength,
    bool horizontalDirection)
  {
    GroundLayer = groundLayer;
    ABSensorLength = aBSensorLength;
    ReversedABSensorLength = reversedABSensorLength;
    HorizontalDirection = horizontalDirection;
  }
}
