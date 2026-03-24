using UnityEngine;

public interface ISensor
{
  bool Enabled { get; set; }
  Vector2 Position { get; }
  Color EnabledColor { get; }
  Color? DisabledColor { get; }
  SensorRay GetRay(char id);
  void Update(Vector2 parentPosition, SensorDef def);
}
