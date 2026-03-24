using System.Collections.Generic;
using UnityEngine;

public class Sensor<TRayId> : ISensor<TRayId>
  where TRayId : struct
{
  private readonly Dictionary<TRayId, SensorRay> _rays = new();

  public bool Enabled { get; set; }
  public Vector2 Position { get; set; }
  public Color EnabledColor { get; set; }
  public Color? DisabledColor { get; set; }

  public SensorRay GetRay(TRayId id)
  {
    return _rays[id];
  }

  public Sensor<TRayId> AddRay(TRayId id, SensorRay ray)
  {
    _rays.Add(id, ray);
    return this;
  }
}
