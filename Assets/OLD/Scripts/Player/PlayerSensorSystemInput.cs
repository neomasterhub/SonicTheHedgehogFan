using UnityEngine;

public readonly struct PlayerSensorSystemInput<TSizeMode>
  where TSizeMode : struct
{
  public readonly bool HorizontalDirection;
  public readonly Vector2 ParentPosition;
  public readonly TSizeMode SizeMode;
  public readonly GroundSide GroundSide;
  public readonly LayerMask GroundLayer;

}
