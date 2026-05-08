using UnityEngine;

public class RingSensorSystem
{
  private const float _rayLength = 0.23f;

  private readonly Sensor _o;

  public RingSensorSystem()
  {
    _o = new(Color.gold, Vector2.zero, Vector2.down);
    _o.Ray.Length = _rayLength;
  }

  public void Update(Vector2 parentPosition)
  {
    _o.SetParentPosition(parentPosition);
  }

  public bool DetectGround(LayerMask groundLayer)
  {
    return _o.Ray.Cast(groundLayer) != null;
  }

  public void Draw()
  {
    _o.Draw();
  }
}
