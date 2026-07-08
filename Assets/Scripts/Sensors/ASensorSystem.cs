using UnityEngine;
using static Helpers.Sensors;
using static SharedConsts.Physics;

public class ASensorSystem
{
  private readonly char _aId;
  private readonly UDSensor _a;
  private readonly Vector2 _aOffset;

  public ASensorSystem(
    Vector2? position = null,
    Color? sensorColor = null,
    float innerSensorRayLength = SensorRayLengths.GroundInner,
    float outerSensorRayLength = SensorRayLengths.GroundOuter,
    char sensorId = 'A')
  {
    _aId = sensorId;
    _aOffset = position ?? Vector2.zero;
    _a = new(sensorColor ?? Color.green, _aOffset, Vector2.up, Vector2.down);
    _a.UpRay.Length = innerSensorRayLength;
    _a.DownRay.Length = outerSensorRayLength;
  }

  public void SetMeshRenderer(IMeshRenderer meshRenderer)
  {
    _a.SetMeshRenderer(meshRenderer);
  }

  public void Draw()
  {
    _a.Draw();
  }

  public void Update(Vector2 parentPosition)
  {
    _a.SetParentPosition(parentPosition);
  }

  public GroundDetectionResult? DetectGround(LayerMask groundLayer)
  {
    return ADetectGround(_aId, _a, groundLayer);
  }
}
