using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SonicSensorSystem
{
  public SonicSensorSystem()
  {
    Sensors = Enum
      .GetValues(typeof(SensorId))
      .Cast<SensorId>()
      .ToDictionary(id => id, id => new SensorInfo());
  }

  public Dictionary<SensorId, SensorInfo> Sensors { get; }

  public void Update(
    Vector2 parent,
    SonicSizeMode sonicSizeMode,
    GroundSide groundSide,
    float length = SonicConsts.Sensors.Length)
  {
    foreach (var sensorDef in SonicConsts.Sensors.Offsets[sonicSizeMode][groundSide])
    {
      Sensors[sensorDef.Key].Update(sensorDef.Value, parent, length, SonicConsts.Sensors.Colors[sensorDef.Key]);
    }
  }

  public void Draw(
    float beginRadius = 0,
    float endRadius = 0)
  {
    foreach (var sensorInfo in Sensors.Values)
    {
      sensorInfo.Draw(beginRadius, endRadius);
    }
  }
}
