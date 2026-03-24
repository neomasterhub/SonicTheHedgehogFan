using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerSensorSystemManager
{
  private readonly Vector2 _smallHVRadii;
  private readonly Vector2 _bigHVRadii;
  private readonly Dictionary<SizeMode, Dictionary<GroundSide, Dictionary<SensorId, SensorDef>>> _sensorsOffsets;

  private Vector2 _hvRadii;
  private ABResult _abResult;
  private PlayerSensorSystemInput _input;

  public PlayerSensorSystemManager(
    Vector2 smallHVRadii,
    Vector2 bigHVRadii,
    Dictionary<SizeMode, Dictionary<GroundSide, Dictionary<SensorId, SensorDef>>> sensorsOffsets)
  {
    _smallHVRadii = smallHVRadii;
    _bigHVRadii = bigHVRadii;
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
      _input.Parent,
      Sensors[SensorId.A].Direction,
      _hvRadii.y + _input.ABSensorLength,
      _input.GroundLayer);
  }

  public void ApplyAB()
  {
    if (!_input.ASettings.Enabled || !_input.BSettings.Enabled)
    {
      _abResult.Reset();
      return;
    }

    SensorId backId;
    SensorId frontId;

    if (_input.HorizontalDirection)
    {
      backId = SensorId.A;
      frontId = SensorId.B;
    }
    else
    {
      backId = SensorId.B;
      frontId = SensorId.A;
    }

    var b = Sensors[backId];
    var f = Sensors[frontId];
    var bs = _input[backId];
    var fs = _input[frontId];

    var bHit = Physics2D.Raycast(b.Begin, b.Direction, bs.Length, _input.GroundLayer);
    var fHit = Physics2D.Raycast(f.Begin, f.Direction, fs.Length, _input.GroundLayer);

    if (bHit && fHit)
    {
      if (bHit.distance <= fHit.distance)
      {
        _abResult.Set(backId, bHit, b.Direction, 1, bs.Length, true);
      }
      else
      {
        _abResult.Set(frontId, fHit, f.Direction, 1, fs.Length, true);
      }

      return;
    }

    var rbHit = Physics2D.Raycast(b.Begin, -b.Direction, bs.ReversedLength, _input.GroundLayer);
    var rfHit = Physics2D.Raycast(f.Begin, -f.Direction, fs.ReversedLength, _input.GroundLayer);

    if (rbHit && rfHit)
    {
      if (rbHit.distance >= rfHit.distance)
      {
        _abResult.Set(backId, rbHit, -b.Direction, -1, bs.ReversedLength, true);
      }
      else
      {
        _abResult.Set(frontId, rfHit, -f.Direction, -1, fs.ReversedLength, true);
      }

      return;
    }

    if (rbHit)
    {
      _abResult.Set(backId, rbHit, -b.Direction, -1, bs.ReversedLength);
      return;
    }

    if (rfHit)
    {
      _abResult.Set(frontId, rfHit, -f.Direction, -1, fs.ReversedLength);
      return;
    }

    if (bHit)
    {
      _abResult.Set(backId, bHit, b.Direction, 1, bs.Length);
      return;
    }

    if (fHit)
    {
      _abResult.Set(frontId, fHit, f.Direction, 1, fs.Length);
      return;
    }

    _abResult.Reset();
  }

  public void Update(PlayerSensorSystemInput input)
  {
    _input = input;
    _hvRadii = _input.SizeMode == SizeMode.Small ? _smallHVRadii : _bigHVRadii;

    foreach (var (key, value) in _sensorsOffsets[_input.SizeMode][_input.GroundSide])
    {
      Sensors[key].Update(
        value,
        _input.Parent,
        _input[key]);
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
