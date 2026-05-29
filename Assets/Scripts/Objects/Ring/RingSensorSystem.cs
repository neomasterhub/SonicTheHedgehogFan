using UnityEngine;
using static Helpers.Sensors;

public class RingSensorSystem
{
  private readonly char _aId;
  private readonly UDSensor _a;
  private readonly Vector2 _aOffset;

  public RingSensorSystem(
    Vector2 position,
    float innerSensorRayLength,
    float outerSensorRayLength,
    char sensorId = 'A')
  {
    _aId = sensorId;
    _aOffset = position;
    _a = new(Color.gold, _aOffset, Vector2.up, Vector2.down);
    _a.UpRay.Length = innerSensorRayLength;
    _a.DownRay.Length = outerSensorRayLength;
  }

  public void Draw()
  {
    _a.Draw();
  }

  public void Update(Vector2 parentPosition)
  {
    _a.SetParentPosition(parentPosition + _aOffset);
  }

  public GroundDetectionResult? DetectGround(LayerMask groundLayer)
  {
    return ADetectGround(_aId, _a, groundLayer);
  }
}
