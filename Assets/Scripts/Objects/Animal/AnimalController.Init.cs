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
    _speedXPx = SpeedXPx;
    _jumpSpeedPx = JumpSpeedPx;
    _releasedSpeedPx = ReleasedSpeedPx;
    _gravitySpeedSpx = GravitySpeedSpx;

    _sensorSystem = new ASensorSystem(
      new(0, SensorY),
      Color.aliceBlue);

    _speedSystem = new(
      _speedXPx / PxPerUnit,
      _jumpSpeedPx / PxPerUnit,
      _gravitySpeedSpx / SpxPerPx);
  }

  private void Awake()
  {
    _speedSystem.Initialize(0, _releasedSpeedPx / PxPerUnit);
  }
}
