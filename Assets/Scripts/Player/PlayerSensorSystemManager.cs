using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerSensorSystemManager
{
  private readonly Vector2 _smallHVRadii;
  private readonly Vector2 _bigHVRadii;
  private readonly Dictionary<SensorId, Color> _sensorsColors;
  private readonly Dictionary<SizeMode, Dictionary<GroundSide, Dictionary<SensorId, SensorDef>>> _sensorsOffsets;

  private Vector2 _hvRadii;
  private ABResult _abResult;
  private PlayerSensorSystemInput _input;

  public PlayerSensorSystemManager(
    Vector2 smallHVRadii,
    Vector2 bigHVRadii,
    Dictionary<SensorId, Color> sensorsColors,
    Dictionary<SizeMode, Dictionary<GroundSide, Dictionary<SensorId, SensorDef>>> sensorsOffsets)
  {
    _smallHVRadii = smallHVRadii;
    _bigHVRadii = bigHVRadii;
    _sensorsColors = sensorsColors;
    _sensorsOffsets = sensorsOffsets;

    Sensors = Enum
      .GetValues(typeof(SensorId))
      .Cast<SensorId>()
      .ToDictionary(id => id, id => new SensorInfo());
  }

  public Dictionary<SensorId, SensorInfo> Sensors { get; }
  public ABResult ABResult => _abResult;

  public bool IsOnGroundEdge()
  {
    if (!_abResult.GroundDetected || _abResult.BothTriggered)
    {
      return false;
    }

    return !Physics2D.Raycast(
      input.Parent,
      Sensors[SensorId.A].Direction,
      _hvRadii.y + input.ABSensorLength,
      input.GroundLayer);
  }

  public void ApplyAB()
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
      _abResult.Set(bHit.distance <= fHit.distance ? bHit : fHit, b.Direction, 1, input.ABSensorLength, true);
      return;
    }

    var rbHit = Physics2D.Raycast(b.Begin, -b.Direction, input.ReversedABSensorLength, input.GroundLayer);
    var rfHit = Physics2D.Raycast(f.Begin, -f.Direction, input.ReversedABSensorLength, input.GroundLayer);

    if (rbHit && rfHit)
    {
      _abResult.Set(rbHit.distance >= rfHit.distance ? rbHit : rfHit, -b.Direction, -1, input.ReversedABSensorLength, true);
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
    _input = input;
    _hvRadii = input.SizeMode == SizeMode.Small ? _smallHVRadii : _bigHVRadii;

    foreach (var sensor in _sensorsOffsets[input.SizeMode][input.GroundSide])
    {
      Sensors[sensor.Key].Update(
        sensor.Value,
        input.Parent,
        input.GetSensorLength(sensor.Key),
        input.GetReversedSensorLength(sensor.Key),
        _sensorsColors[sensor.Key]);
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
