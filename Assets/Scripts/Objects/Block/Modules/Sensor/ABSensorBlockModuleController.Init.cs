using UnityEngine;
using static SharedConsts.Physics;

/// <summary>
/// Init.
/// </summary>
public partial class ABSensorBlockModuleController
{
  public ABSensorBlockModuleController()
    : base()
  {
    _a = new(Color.green, Vector2.zero, Vector2.up, Vector2.down, Vector2.left);
    _b = new(Color.softGreen, Vector2.zero, Vector2.up, Vector2.down, Vector2.right);
  }

  public override void Initialize(BlockControllerBase context)
  {
    base.Initialize(context);

    InitializeComponents();
    InitializeSensors();
  }

  private void InitializeComponents()
  {
    _spriteRenderer = GetComponent<SpriteRenderer>();
  }

  private void InitializeSensors()
  {
    var sensorOffset = new Vector2(_abDistance / 2, 0);
    InitializeSensor(_a, -sensorOffset);
    InitializeSensor(_b, sensorOffset);
  }

  private void InitializeSensor(UDFSensor sensor, Vector2 offset)
  {
    sensor.SetLocalPosition(_position + offset);
    sensor.UpRay.Length = SensorRayLengths.GroundInner;
    sensor.DownRay.Length = SensorRayLengths.GroundOuter;
    sensor.FrontRay.Length = SensorRayLengths.GroundFront;
  }
}
