using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerSensorSystemManager
{
  private ABResult _abResult;

  public PlayerSensorSystemManager()
  {
    Sensors = Enum
      .GetValues(typeof(SensorId))
      .Cast<SensorId>()
      .ToDictionary(id => id, id => new SensorInfo());
  }

  public Dictionary<SensorId, SensorInfo> Sensors { get; }
  public ABResult ABResult => _abResult;

  public void ApplyAB(PlayerSensorSystemInput input)
  {
    SensorInfo b; // back
    SensorInfo f; // front

    if (input.HorizontalDirection)
    {
      b = Sensors[SensorId.A];
      f = Sensors[SensorId.B];
    }
    else
    {
      b = Sensors[SensorId.B];
      f = Sensors[SensorId.A];
    }

    var bHit = Physics2D.Raycast(b.Begin, b.Direction, input.ABSensorLength, input.GroundLayer);
    var fHit = Physics2D.Raycast(f.Begin, f.Direction, input.ABSensorLength, input.GroundLayer);

    if (bHit && fHit)
    {
      _abResult.Set(bHit.distance <= fHit.distance ? bHit : fHit, b.Direction, 1, input.ABSensorLength);
      return;
    }

    var rbHit = Physics2D.Raycast(b.Begin, -b.Direction, input.ReversedABSensorLength, input.GroundLayer);
    var rfHit = Physics2D.Raycast(f.Begin, -f.Direction, input.ReversedABSensorLength, input.GroundLayer);

    if (rbHit && rfHit)
    {
      _abResult.Set(rbHit.distance >= rfHit.distance ? rbHit : rfHit, -b.Direction, -1, input.ReversedABSensorLength);
      return;
    }

    if (rbHit)
    {
      _abResult.Set(rbHit, -b.Direction, -1, input.ReversedABSensorLength);
      return;
    }

    if (rfHit)
    {
      _abResult.Set(rfHit, -f.Direction, -1, input.ReversedABSensorLength);
      return;
    }

    if (bHit)
    {
      _abResult.Set(bHit, b.Direction, 1, input.ABSensorLength);
      return;
    }

    if (fHit)
    {
      _abResult.Set(fHit, f.Direction, 1, input.ABSensorLength);
      return;
    }

    _abResult.Reset();
  }

  public void Update(PlayerSensorSystemInput input)
  {
    foreach (var sensor in SonicConsts.Sensors.Offsets[input.SizeMode][input.GroundSide])
    {
      Sensors[sensor.Key].Update(
        sensor.Value,
        input.Parent,
        input.GetSensorLength(sensor.Key),
        input.GetReversedSensorLength(sensor.Key),
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
    _abResult.DrawNormal(length, beginRadius, endRadius, color);
  }
}
