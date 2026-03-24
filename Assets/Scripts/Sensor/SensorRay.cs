using UnityEngine;

public struct SensorRay<TId>
  where TId : struct
{
  public bool Enabled;
  public float Length;
  public Vector2 Direction;
}
