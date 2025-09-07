using UnityEngine;

public readonly struct SensorDef
{
  public readonly Vector2 Offset;
  public readonly Vector2 Direction;

  public SensorDef(Vector2 offset, Vector2 direction)
  {
    Offset = offset;
    Direction = direction;
  }
}
