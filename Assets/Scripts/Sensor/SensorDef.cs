using System.Collections.Generic;
using UnityEngine;

public class SensorDef
{
  private readonly Dictionary<char, Vector2> _rayDirections = new();

  public SensorDef(Vector2 offset)
  {
    Offset = offset;
  }

  public Vector2 Offset { get; private set; }
  public IReadOnlyDictionary<char, Vector2> RayDirections => _rayDirections;

  public SensorDef AddRayDirection(char id, Vector2 direction)
  {
    _rayDirections.Add(id, direction);
    return this;
  }
}
