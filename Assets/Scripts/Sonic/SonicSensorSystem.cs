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

  public void ApplyAB(LayerMask groundLayer)
  {
    var a = Sensors[SensorId.A];
    var b = Sensors[SensorId.B];

    var aHit = Physics2D.Raycast(a.Begin, a.Direction, a.Length, groundLayer);
    var bHit = Physics2D.Raycast(b.Begin, b.Direction, b.Length, groundLayer);

    if (!aHit && !bHit)
    {
      _abResult.Reset();
      return;
    }

    RaycastHit2D hit = aHit && bHit
      ? (aHit.distance < bHit.distance ? aHit : bHit)
      : (aHit ? aHit : bHit);

    _abResult.Contact = hit.point;
    _abResult.Normal = hit.normal;
    _abResult.AngleDeg = Vector2.SignedAngle(-a.Direction, hit.normal);
    _abResult.AngleRad = _abResult.AngleDeg * Mathf.Deg2Rad;
    _abResult.GroundDetected = true;
  }

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
