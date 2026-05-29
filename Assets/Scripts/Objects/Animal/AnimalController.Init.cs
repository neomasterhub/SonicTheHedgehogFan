using UnityEngine;
using static AnimalConsts;
using static SharedConsts.ConvertValues;

/// <summary>
/// Init.
/// </summary>
public partial class AnimalController
{
  public AnimalController()
  {
    _sensorSystem = new ASensorSystem(new(0, SensorY), Color.aliceBlue);
    _speedSystem = new();
  }

  private void Awake()
  {
    GetComponent<SpriteRenderer>().flipX = _speedXPx < 0;

    _speedSystem.Initialize(
      _speedXPx / PxPerUnit,
      _releaseSpeedPx / PxPerUnit,
      _jumpSpeedPx / PxPerUnit,
      _gravitySpeedSpx / SpxPerUnit);
  }
}
