using UnityEngine;
using static RingConsts.Physics;

public class RingSensorSystem
{
  private readonly Sensor _o;

  public RingSensorSystem()
  {
    _o = new(Color.gold, Vector2.zero, Vector2.down);
    _o.Ray.Length = SensorRayLength;
  }

  public void Draw()
  {
    _o.Draw();
  }

  public void Update(Vector2 parentPosition)
  {
    _o.SetParentPosition(parentPosition);
  }

  public GroundDetectionResult? DetectGround(LayerMask groundLayer)
  {
    var hit = _o.Ray.Cast(groundLayer);

    return hit.HasValue
      ? new(false, hit.Value, _o.Ray.Direction)
      : null;
  }
}
