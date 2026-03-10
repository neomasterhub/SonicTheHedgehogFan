using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SonicSensorSystem
{
  private ABResult _abResult;

  public SonicSensorSystem()
  {
    Sensors = Enum
      .GetValues(typeof(SensorId))
      .Cast<SensorId>()
      .ToDictionary(id => id, id => new SensorInfo());
  }

  public ABResult ABResult => _abResult;
  public Dictionary<SensorId, SensorInfo> Sensors { get; }

  public void ApplyAB(PlayerSensorSystemInput input)
  {
    var a = Sensors[SensorId.A];
    var b = Sensors[SensorId.B];

    var aHit = Physics2D.Raycast(a.Begin, a.Direction, input.ABSensorLength, input.GroundLayer);
    var bHit = Physics2D.Raycast(b.Begin, b.Direction, input.ABSensorLength, input.GroundLayer);

    if (aHit && bHit)
    {
      _abResult.Set(aHit.distance < bHit.distance ? aHit : bHit, a.Direction, 1, sensorLength);
      return;
    }

    var raHit = Physics2D.Raycast(a.Begin, -a.Direction, input.ReversedABSensorLength, input.GroundLayer);
    var rbHit = Physics2D.Raycast(b.Begin, -b.Direction, input.ReversedABSensorLength, input.GroundLayer);

    if (raHit && rbHit)
    {
      _abResult.Set(raHit.distance > rbHit.distance ? raHit : rbHit, -a.Direction, -1, reversedSensorLength);
      return;
    }

    if (raHit)
    {
      _abResult.Set(raHit, -a.Direction, -1, reversedSensorLength);
      return;
    }

    if (rbHit)
    {
      _abResult.Set(rbHit, -b.Direction, -1, reversedSensorLength);
      return;
    }

    if (aHit)
    {
      _abResult.Set(aHit, a.Direction, 1, sensorLength);
      return;
    }

    if (bHit)
    {
      _abResult.Set(bHit, b.Direction, 1, sensorLength);
      return;
    }

    _abResult.Reset();
  }

  public void Update(
    Vector2 parent,
    SonicSizeMode sonicSizeMode,
    GroundSide groundSide,
    float sensorLength,
    float reversedSensorLength)
  {
    foreach (var sensor in SonicConsts.Sensors.Offsets[sonicSizeMode][groundSide])
    {
      Sensors[sensor.Key].Update(
        sensor.Value,
        parent,
        sensorLength,
        reversedSensorLength,
        SonicConsts.Sensors.Colors[sensor.Key]);
    }
  }

  public void DrawSensors(
    float beginRadius = 0,
    float endRadius = 0)
  {
    foreach (var sensorInfo in Sensors.Values)
    {
      sensorInfo.Draw(beginRadius, endRadius);
    }
  }

  public void DrawGroundNormal(
    float length = 1,
    float beginRadius = 0,
    float endRadius = 0,
    Color? color = null)
  {
    ABResult.Draw(length, beginRadius, endRadius, color);
  }
}
