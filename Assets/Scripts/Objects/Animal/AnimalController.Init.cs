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
    GetComponent<SpriteRenderer>().flipX = _horizontalSpeedPx < 0;

    _speedSystem.Initialize(
      _horizontalSpeedPx / PxPerUnit,
      _releaseSpeedPx / PxPerUnit,
      _jumpSpeedPx / PxPerUnit,
      _gravitySpeedSpx / SpxPerUnit);
  }
}
