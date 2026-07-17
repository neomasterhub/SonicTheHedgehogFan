/// <summary>
/// Pipeline.
/// </summary>
public partial class EdgeColliderRendererController
{
  private void LateUpdate()
  {
    Draw();
  }

  private void Draw()
  {
    for (var i = 0; i < _edgeColliders.Length; i++)
    {
      var ec = _edgeColliders[i];

      var points = ec.points;
      var transform = ec.transform;

      for (var j = 0; j < points.Length - 1; j++)
      {
        _meshRenderer.DrawLine(
          transform.TransformPoint(points[j]),
          transform.TransformPoint(points[j + 1]),
          _width,
          _color);
      }
    }
  }
}
