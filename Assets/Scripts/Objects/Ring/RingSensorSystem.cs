using UnityEngine;
using static RingConsts.Physics;

public class RingSensorSystem
{
  private const char _oId = 'O';

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

  public GroundDetectionResult? DetectGround(LayerMask groundLayer)
  {
    var hit = _o.DownRay.Cast(groundLayer);
    if (hit != null)
    {
      return new(_oId, hit.Value, _o.DownRay.Direction, VerticalRelation.Above);
    }

    hit = _o.UpRay.Cast(groundLayer);
    if (hit != null)
    {
      return new(_oId, hit.Value, _o.UpRay.Direction, VerticalRelation.Below);
    }

    return null;
  }
}
