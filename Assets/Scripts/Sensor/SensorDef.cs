using System.Collections.Generic;
using UnityEngine;

public class SensorDef
{
  private readonly Dictionary<char, SensorRay> _rays = new();

  public SensorDef(Vector2 offset)
  {
    Offset = offset;
  }

  public Vector2 Offset { get; private set; }
  public IReadOnlyDictionary<char, SensorRay> Rays => _rays;

  public SensorDef AddRay(char id, SensorRay ray)
  {
    _rays.Add(id, ray);
    return this;
  }
}
