using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SonicSensorSystem
{
  private readonly LayerMask _groundLayer;

  public SonicSensorSystem(LayerMask groundLayer)
  {
    _groundLayer = groundLayer;

    Sensors = Enum
      .GetValues(typeof(SensorId))
      .Cast<SensorId>()
      .ToDictionary(id => id, id => new SensorInfo());
  }

  public Dictionary<SensorId, SensorInfo> Sensors { get; }

  public ABResult? ApplyAB()
  {
    var a = Sensors[SensorId.A];
    var b = Sensors[SensorId.B];

    var aHit = Physics2D.Raycast(a.Begin, a.Direction, a.Length, _groundLayer);
    var bHit = Physics2D.Raycast(b.Begin, b.Direction, b.Length, _groundLayer);

    if (!aHit && !bHit)
    {
      return null;
    }

    RaycastHit2D hit = aHit && bHit
      ? (aHit.distance < bHit.distance ? aHit : bHit)
      : (aHit ? aHit : bHit);

    var normal = hit.normal;
    var angleDeg = Vector2.SignedAngle(-a.Direction, hit.normal);
    var angleRad = angleDeg * Mathf.Deg2Rad;

    return new ABResult(normal, angleDeg, angleRad);
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
