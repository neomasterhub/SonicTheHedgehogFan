using UnityEngine;

public class SensorInfo
{
  public Vector2 Offset { get; private set; }
  public Vector2 Direction { get; private set; }
  public Vector2 Begin { get; private set; }
  public Vector2 End { get; private set; }
  public Color Color { get; private set; }
  public float Length { get; private set; }

  public void Update(
    SensorDef sensorDef,
    Vector2 parent,
    float length,
    Color? color = null)
  {
    Offset = sensorDef.Offset;
    Direction = sensorDef.Direction;

    Begin = parent + sensorDef.Offset;
    End = Begin + (sensorDef.Direction * length);

    Color = color ?? Color.yellow;
    Length = length;
  }

  public void Draw(
    float beginRadius = 0,
    float endRadius = 0)
  {
    Gizmos.color = Color;
    Gizmos.DrawLine(Begin, End);
    Gizmos.DrawSphere(Begin, beginRadius);
    Gizmos.DrawSphere(End, endRadius);
  }
}
