using UnityEngine;

public class SensorInfo
{
  public bool Enabled { get; private set; }
  public Color EnabledColor { get; private set; }
  public Color DisabledColor { get; private set; }
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
    SensorSettings sensorSettings)
  {
    Enabled = sensorSettings.Enabled;
    EnabledColor = sensorSettings.EnabledColor;
    DisabledColor = sensorSettings.DisabledColor;
    Offset = sensorDef.Offset;
    Direction = sensorDef.Direction;

    Begin = parent + sensorDef.Offset;
    End = Begin + (sensorDef.Direction * sensorSettings.Length);
    ReversedEnd = Begin - (sensorDef.Direction * sensorSettings.ReversedLength);

    Length = sensorSettings.Length;
    ReversedLength = sensorSettings.ReversedLength;
  }

  public void Draw(
    float beginRadius = 0,
    float endRadius = 0)
  {
    if (Enabled)
    {
      Gizmos.color = EnabledColor;
      Gizmos.DrawLine(ReversedEnd, End);
      Gizmos.DrawSphere(Begin, beginRadius);
      Gizmos.DrawSphere(End, endRadius);
      Gizmos.DrawSphere(ReversedEnd, endRadius);
    }
    else
    {
      Gizmos.color = DisabledColor;
      Gizmos.DrawSphere(Begin, beginRadius);
    }
  }
}
