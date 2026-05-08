using UnityEngine;

public class RingSensorSystem
{
  private readonly Sensor _o;

  public RingSensorSystem()
  {
    _o = new(Color.gold, Vector2.zero, Vector2.down);
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
