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
    _viewSystem = new();
  }

  private void Awake()
  {
    InitializeComponents();
    InitializeSpeedSystem();
  }

  private void InitializeComponents()
  {
    GetComponent<SpriteRenderer>().flipX = !_horizontalDirection;
    _viewSystem.InitializeComponents(GetComponent<Animator>());
  }

  private void InitializeSpeedSystem()
  {
    _speedSystem.Initialize(
      (_horizontalDirection ? _horizontalSpeedPx : -_horizontalSpeedPx) / PxPerUnit,
      _releaseSpeedPx / PxPerUnit,
      _jumpSpeedPx / PxPerUnit,
      _gravitySpeedSpx / SpxPerUnit);
  }
}
