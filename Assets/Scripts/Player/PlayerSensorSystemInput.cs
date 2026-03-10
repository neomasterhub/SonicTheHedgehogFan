using UnityEngine;

public readonly struct PlayerSensorSystemInput
{
  public readonly Vector2 Parent;
  public readonly SizeMode SizeMode;
  public readonly LayerMask GroundLayer;
  public readonly float ABSensorLength;
  public readonly float ReversedABSensorLength;
  public readonly bool HorizontalDirection;

  public PlayerSensorSystemInput(
    Vector2 parent,
    SizeMode sizeMode,
    LayerMask groundLayer,
    float abSensorLength,
    float reversedABSensorLength,
    bool horizontalDirection)
  {
    Parent = parent;
    SizeMode = sizeMode;
    GroundLayer = groundLayer;
    ABSensorLength = abSensorLength;
    ReversedABSensorLength = reversedABSensorLength;
    HorizontalDirection = horizontalDirection;
  }
}
