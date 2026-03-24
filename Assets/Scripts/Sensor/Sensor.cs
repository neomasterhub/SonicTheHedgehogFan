using System.Collections.Generic;
using UnityEngine;

public class Sensor : ISensor
{
  private readonly Dictionary<char, SensorRay> _rays = new();

  public bool Enabled { get; set; }
  public Vector2 Position { get; set; }
  public Color EnabledColor { get; set; }
  public Color? DisabledColor { get; set; }

  public SensorRay GetRay(char id)
  {
    return _rays[id];
  }

  public Sensor AddRay(char id, SensorRay ray)
  {
    _rays.Add(id, ray);
    return this;
  }
}
