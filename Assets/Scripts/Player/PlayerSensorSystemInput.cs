using UnityEngine;

public readonly struct PlayerSensorSystemInput
{
  public readonly Vector2 Parent;
  public readonly SizeMode SizeMode;
  public readonly GroundSide GroundSide;
  public readonly LayerMask GroundLayer;
  public readonly float ABSensorLength;
  public readonly float CDSensorLength;
  public readonly float EFSensorLength;
  public readonly float ReversedABSensorLength;
  public readonly float ReversedCDSensorLength;
  public readonly float ReversedEFSensorLength;
  public readonly bool HorizontalDirection;

  public PlayerSensorSystemInput(Vector2 parent, SizeMode sizeMode, GroundSide groundSide, LayerMask groundLayer, float aBSensorLength, float cDSensorLength, float eFSensorLength, float reversedABSensorLength, float reversedCDSensorLength, float reversedEFSensorLength, bool horizontalDirection)
  {
    Parent = parent;
    SizeMode = sizeMode;
    GroundSide = groundSide;
    GroundLayer = groundLayer;
    ABSensorLength = aBSensorLength;
    CDSensorLength = cDSensorLength;
    EFSensorLength = eFSensorLength;
    ReversedABSensorLength = reversedABSensorLength;
    ReversedCDSensorLength = reversedCDSensorLength;
    ReversedEFSensorLength = reversedEFSensorLength;
    HorizontalDirection = horizontalDirection;
  }

  public float GetSensorLength(SensorId id)
  {
    return id switch
    {
      SensorId.A or SensorId.B => ABSensorLength,
      SensorId.C or SensorId.D => CDSensorLength,
      SensorId.E or SensorId.F => EFSensorLength,
      _ => throw id.ArgumentOutOfRangeException(),
    };
  }

  public float GetReversedSensorLength(SensorId id)
  {
    return id switch
    {
      SensorId.A or SensorId.B => ReversedABSensorLength,
      SensorId.C or SensorId.D => ReversedCDSensorLength,
      SensorId.E or SensorId.F => ReversedEFSensorLength,
      _ => throw id.ArgumentOutOfRangeException(),
    };
  }
}
