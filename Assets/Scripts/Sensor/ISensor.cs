using UnityEngine;

public interface ISensor<TRayId>
  where TRayId : struct
{
  bool Enabled { get; set; }
  Vector2 Offset { get; set; }
  Color EnabledColor { get; set; }
  Color? DisabledColor { get; set; }
  SensorRay<TRayId> GetRay(TRayId id);
}
