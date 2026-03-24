using UnityEngine;

public interface ISensor
{
  bool Enabled { get; set; }
  Vector2 Position { get; set; }
  Color EnabledColor { get; set; }
  Color? DisabledColor { get; set; }
  SensorRay GetRay(char id);
}
