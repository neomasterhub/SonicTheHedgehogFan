using UnityEngine;
using static EnemyConsts.Physics;

/// <summary>
/// Init.
/// </summary>
public partial class ABSensorEnemyModuleController
{
  public ABSensorEnemyModuleController()
    : base()
  {
    _a = new(Color.orangeRed, Vector2.zero, Vector2.up, Vector2.down, Vector2.left);
    _b = new(Color.red, Vector2.zero, Vector2.up, Vector2.down, Vector2.right);
  }

  public override void Initialize(IEnemyContext context)
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
    sensor.UpRay.Length = UDFLengths.x;
    sensor.DownRay.Length = UDFLengths.y;
    sensor.FrontRay.Length = UDFLengths.z;
  }
}
