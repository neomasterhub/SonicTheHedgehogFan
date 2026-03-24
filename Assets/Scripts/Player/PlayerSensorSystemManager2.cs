using System.Collections.Generic;
using UnityEngine;

public class PlayerSensorSystemManager2<TSizeMode>
  where TSizeMode : struct
{
  private readonly Dictionary<SensorId, Sensor> _sensors = new();

  public PlayerSensorSystemManager2()
  {
    _sensors.Add(SensorId.A, new(Color.limeGreen));
    _sensors.Add(SensorId.B, new(Color.green));
  }
}
