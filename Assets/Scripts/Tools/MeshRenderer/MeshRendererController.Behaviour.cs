using UnityEngine;

/// <summary>
/// Behaviour.
/// </summary>
public partial class MeshRendererController : IMeshRenderer
{
  public void DrawPolygon(Vector3[] points, Color color)
  {
    if (points == null || points.Length < 3)
    {
      return;
    }

    var vi = _vertices.Count;

    for (var i = 0; i < points.Length; i++)
    {
      _vertices.Add(points[i]);
      _colors.Add(color);
    }

    for (var i = 1; i < points.Length - 1; i++)
    {
      _triangles.Add(vi);
      _triangles.Add(vi + i);
      _triangles.Add(vi + i + 1);
    }
  }
}
