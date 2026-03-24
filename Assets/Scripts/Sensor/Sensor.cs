using System.Collections.Generic;
using UnityEngine;

public class Sensor : ISensor
{
  private IReadOnlyDictionary<char, SensorRay> _rays;

  public Sensor(Color enabledColor, Color? disabledColor = null, bool enabled = true)
  {
    Enabled = enabled;
    EnabledColor = enabledColor;
    DisabledColor = disabledColor;
  }

  public bool Enabled { get; set; }
  public Vector2 Position { get; private set; }
  public Color EnabledColor { get; private set; }
  public Color? DisabledColor { get; private set; }

  public SensorRay GetRay(char id)
  {
    return _rays[id];
  }

  public void Update(Vector2 parentPosition, SensorDef def)
  {
    _rays = def.Rays;
    Position = parentPosition + def.Offset;
  }
}
