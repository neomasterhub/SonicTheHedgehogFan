using UnityEngine;
using static AnimalConsts;

/// <summary>
/// Init.
/// </summary>
public partial class AnimalController
{
  public AnimalController()
  {
    _sensorSystem = new ASensorSystem(
      new(0, SensorY),
      Color.aliceBlue);

    _speedSystem = new(
      _speedX ?? SpeedX,
      _jumpSpeed ?? JumpSpeed,
      _gravitySpeed ?? GravitySpeed);
  }

  private void Awake()
  {
    _speedSystem.Initialize(_initialSpeed.x, _initialSpeed.y);
  }
}
