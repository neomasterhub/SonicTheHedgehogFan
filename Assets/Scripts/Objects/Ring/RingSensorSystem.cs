using UnityEngine;
using static RingConsts.Physics;

public class RingSensorSystem
{
  private readonly UDSensor _a;
  private readonly Vector2 _aOffset;

  public RingSensorSystem()
  {
    _aOffset = new(0, -SensorY);
    _a = new(Color.gold, _aOffset, Vector2.up, Vector2.down);
    _a.UpRay.Length = InnerSensorRayLength;
    _a.DownRay.Length = OuterSensorRayLength;
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
    var hit = _a.DownRay.Cast(groundLayer);
    if (hit != null)
    {
      return new(false, hit.Value, _a.DownRay.Direction, VerticalRelation.Above);
    }

    hit = _a.UpRay.Cast(groundLayer);
    if (hit != null)
    {
      return new(false, hit.Value, _a.UpRay.Direction, VerticalRelation.Below);
    }

    return null;
  }
}
