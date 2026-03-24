using System.Collections.Generic;
using UnityEngine;

public class PlayerSensorSystemManager2<TSizeMode>
  where TSizeMode : struct
{
  private readonly Dictionary<SensorId, Sensor> _sensors = new();
  private readonly Dictionary<TSizeMode, Dictionary<GroundSide, Dictionary<SensorId, SensorDef>>> _sensorDefs;

  public PlayerSensorSystemManager2(Dictionary<TSizeMode, Dictionary<GroundSide, Dictionary<SensorId, SensorDef>>> sensorDefs)
  {
    _sensorDefs = sensorDefs;
    _sensors.Add(SensorId.A, new(Color.limeGreen));
    _sensors.Add(SensorId.B, new(Color.green));
  }
}
