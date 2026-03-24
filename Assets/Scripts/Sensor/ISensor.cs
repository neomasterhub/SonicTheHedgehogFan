using UnityEngine;

public interface ISensor<TRayId>
  where TRayId : struct
{
  bool Enabled { get; set; }
  Vector2 Position { get; set; }
  Color EnabledColor { get; set; }
  Color? DisabledColor { get; set; }
  SensorRay GetRay(TRayId id);
}
