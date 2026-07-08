using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[DefaultExecutionOrder(1000)]
public class MeshRendererController
  : MonoBehaviour,
  IMeshRenderer
{
  private readonly List<Vector3> _vertices = new();
  private readonly List<Color> _colors = new();
  private readonly List<int> _triangles = new();

  private Mesh _mesh;
  private MeshFilter _meshFilter;
  private MeshRenderer _meshRenderer;

  private void Awake()
  {
    InitializeComponents();
  }

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

  private void InitializeComponents()
  {
    _mesh = new();
    _mesh.MarkDynamic();
    _meshFilter = this.AddComponent<MeshFilter>();
    _meshRenderer = this.AddComponent<MeshRenderer>();
    _meshRenderer.sharedMaterial = new(Shader.Find("Sprites/Default"));
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
