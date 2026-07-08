using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Data.
/// </summary>
[DefaultExecutionOrder(1000)]
public partial class MeshRendererController
  : MonoBehaviour,
  IMeshRenderer
{
  private readonly List<Vector3> _vertices = new();
  private readonly List<Color> _colors = new();
  private readonly List<int> _triangles = new();

  private Mesh _mesh;
  private MeshFilter _meshFilter;
  private MeshRenderer _meshRenderer;

  private void LateUpdate()
  {
    if (_vertices.Count == 0)
    {
      return;
    }

    _mesh.Clear();
    _mesh.SetVertices(_vertices);
    _mesh.SetColors(_colors);
    _mesh.SetTriangles(_triangles, 0, false);

    _meshFilter.sharedMesh = _mesh;

    ClearMeshData();
  }

  private void ClearMeshData()
  {
    _vertices.Clear();
    _colors.Clear();
    _triangles.Clear();
  }

  public void DrawPolygon(Vector3[] points, Color color, float alpha = 1)
  {
    if (points == null || points.Length < 3)
    {
      return;
    }

    color.a = alpha;

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
