using UnityEngine;

public class SensorInfo
{
  public Color Color { get; private set; }
  public Vector2 Offset { get; private set; }
  public Vector2 Direction { get; private set; }
  public Vector2 Begin { get; private set; }
  public Vector2 End { get; private set; }
  public Vector2 ReversedEnd { get; private set; }
  public float Length { get; private set; }
  public float ReversedLength { get; private set; }

  public void Update(
    SensorDef sensorDef,
    Vector2 parent,
    float length,
    float reversedLength = 0,
    Color? color = null)
  {
    Color = color ?? Color.yellow;
    Offset = sensorDef.Offset;
    Direction = sensorDef.Direction;

    Begin = parent + sensorDef.Offset;
    End = Begin + (sensorDef.Direction * length);
    ReversedEnd = Begin - (sensorDef.Direction * reversedLength);

    Length = length;
    ReversedLength = reversedLength;
  }

  public void Draw(
    float beginRadius = 0,
    float endRadius = 0)
  {
    Gizmos.color = Color;
    Gizmos.DrawLine(ReversedEnd, End);
    Gizmos.DrawSphere(Begin, beginRadius);
    Gizmos.DrawSphere(End, endRadius);
    Gizmos.DrawSphere(ReversedEnd, endRadius);
  }
}
