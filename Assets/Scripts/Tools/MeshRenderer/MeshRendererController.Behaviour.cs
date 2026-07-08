using UnityEngine;

/// <summary>
/// Behaviour.
/// </summary>
public partial class MeshRendererController : IMeshRenderer
{
  public void DrawLine(Vector3 start, Vector3 end, float width, Color color)
  {
    var normal = 0.5f * width * Vector3.Cross((end - start).normalized, Vector3.forward);
    var vi = _vertices.Count;

    _vertices.Add(start + normal);
    _vertices.Add(start - normal);
    _vertices.Add(end - normal);
    _vertices.Add(end + normal);

    _colors.Add(color);
    _colors.Add(color);
    _colors.Add(color);
    _colors.Add(color);

    _triangles.Add(vi);
    _triangles.Add(vi + 1);
    _triangles.Add(vi + 2);

    _triangles.Add(vi);
    _triangles.Add(vi + 2);
    _triangles.Add(vi + 3);
  }

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

  public void DrawSquare(Vector3 position, float halfSize, Color color)
  {
    var vi = _vertices.Count;

    _vertices.Add(new Vector3(position.x - halfSize, position.y - halfSize, position.z));
    _vertices.Add(new Vector3(position.x + halfSize, position.y - halfSize, position.z));
    _vertices.Add(new Vector3(position.x + halfSize, position.y + halfSize, position.z));
    _vertices.Add(new Vector3(position.x - halfSize, position.y + halfSize, position.z));

    _colors.Add(color);
    _colors.Add(color);
    _colors.Add(color);
    _colors.Add(color);

    _triangles.Add(vi);
    _triangles.Add(vi + 1);
    _triangles.Add(vi + 2);

    _triangles.Add(vi);
    _triangles.Add(vi + 2);
    _triangles.Add(vi + 3);
  }
}
