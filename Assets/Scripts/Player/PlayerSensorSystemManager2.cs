using System.Collections.Generic;
using UnityEngine;

public class PlayerSensorSystemManager2<TSizeMode>
  where TSizeMode : struct
{
  private readonly Dictionary<SensorId, Sensor> _sensors = new();
  private readonly Dictionary<TSizeMode, Dictionary<GroundSide, Dictionary<SensorId, SensorDef>>> _sensorDefs;

  private PlayerSensorSystemInput<TSizeMode> _input;

  public PlayerSensorSystemManager2(Dictionary<TSizeMode, Dictionary<GroundSide, Dictionary<SensorId, SensorDef>>> sensorDefs)
  {
    _sensorDefs = sensorDefs;
    _sensors.Add(SensorId.A, new(Color.limeGreen));
    _sensors.Add(SensorId.B, new(Color.green));
    _sensors.Add(SensorId.E, new(Color.yellow));
    _sensors.Add(SensorId.F, new(Color.orange));
  }

  public void Update(PlayerSensorSystemInput<TSizeMode> input)
  {
    _input = input;

    foreach (var (key, value) in _sensorDefs[_input.SizeMode][_input.GroundSide])
    {
      _sensors[key].Update(_input.ParentPosition, value);
    }
  }
}
