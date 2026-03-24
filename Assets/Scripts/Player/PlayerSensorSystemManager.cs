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
  private WallSensorsResult _wallSensorsResult;
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

  public void ApplyWallSensors()
  {
    ApplyWallSensor(Sensors[SensorId.C]);
    ApplyWallSensor(Sensors[SensorId.D]);
  }

  public bool IsOnGroundEdge()
  {
    if (!_abResult.GroundDetected || _abResult.BothTriggered)
    {
      return false;
    }

    var appliedSensor = Sensors[_abResult.AppliedSensorId.Value];

    return !Physics2D.Raycast(
      _input.Parent,
      appliedSensor.Direction,
      _hvRadii.y + appliedSensor.Length,
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

    var bHit = Physics2D.Raycast(b.Begin, b.Direction, b.Length, _input.GroundLayer);
    var fHit = Physics2D.Raycast(f.Begin, f.Direction, f.Length, _input.GroundLayer);

    if (bHit && fHit)
    {
      if (bHit.distance <= fHit.distance)
      {
        _abResult.Set(backId, bHit, b.Direction, 1, b.Length, true);
      }
      else
      {
        _abResult.Set(frontId, fHit, f.Direction, 1, f.Length, true);
      }

      return;
    }

    var rbHit = Physics2D.Raycast(b.Begin, -b.Direction, b.ReversedLength, _input.GroundLayer);
    var rfHit = Physics2D.Raycast(f.Begin, -f.Direction, f.ReversedLength, _input.GroundLayer);

    if (rbHit && rfHit)
    {
      if (rbHit.distance >= rfHit.distance)
      {
        _abResult.Set(backId, rbHit, -b.Direction, -1, b.ReversedLength, true);
      }
      else
      {
        _abResult.Set(frontId, rfHit, -f.Direction, -1, f.ReversedLength, true);
      }

      return;
    }

    if (rbHit)
    {
      _abResult.Set(backId, rbHit, -b.Direction, -1, b.ReversedLength);
      return;
    }

    if (rfHit)
    {
      _abResult.Set(frontId, rfHit, -f.Direction, -1, f.ReversedLength);
      return;
    }

    if (bHit)
    {
      _abResult.Set(backId, bHit, b.Direction, 1, b.Length);
      return;
    }

    if (fHit)
    {
      _abResult.Set(frontId, fHit, f.Direction, 1, f.Length);
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

  public void DrawSensors(float sourceRadius)
  {
    foreach (var sensorInfo in Sensors.Values)
    {
      sensorInfo.Draw(sourceRadius);
    }
  }

  public void DrawGroundNormal(
    float length = 1,
    float sourceRadius = 1,
    Color? color = null)
  {
    _abResult.DrawNormal(length, sourceRadius, color);
  }

  private void ApplyWallSensor(SensorInfo si)
  {
    if (!si.Enabled)
    {
      return;
    }

    var hit = Physics2D.Raycast(si.Begin, si.Direction, si.Length, _input.GroundLayer);
  }
}
