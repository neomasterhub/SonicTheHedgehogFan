using UnityEngine;
using static Helpers.Sensors;
using static RingConsts.Physics;

public class RingSensorSystem
{
  private readonly UDSensor _o;
  private readonly Vector2 _oOffset;

  public RingSensorSystem()
  {
    _oOffset = new(0, -SensorY);
    _o = new(Color.gold, _oOffset, Vector2.up, Vector2.down);
    _o.UpRay.Length = InnerSensorRayLength;
    _o.DownRay.Length = OuterSensorRayLength;
  }

  public void Draw()
  {
    _o.Draw();
  }

  public void Update(Vector2 parentPosition)
  {
    _o.SetParentPosition(parentPosition + _oOffset);
  }

  public GroundDetectionResult? DetectGround(LayerMask groundLayer, char sensorId = 'A')
  {
    return ADetectGround(sensorId, _o, groundLayer);
  }
}
