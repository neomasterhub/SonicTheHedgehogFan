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
    var dir = _a.DownRay.Direction;
    var hit = _a.DownRay.Cast(groundLayer);

    if (hit == null)
    {
      dir = _a.UpRay.Direction;
      hit = _a.UpRay.Cast(groundLayer);
    }

    return hit.HasValue
      ? new(false, hit.Value, dir)
      : null;
  }
}
