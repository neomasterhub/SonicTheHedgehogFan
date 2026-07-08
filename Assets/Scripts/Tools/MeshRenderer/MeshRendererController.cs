using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Data.
/// </summary>
[DefaultExecutionOrder(1000)]
public partial class MeshRendererController : MonoBehaviour
{
  private readonly List<Vector3> _vertices = new();
  private readonly List<Color> _colors = new();
  private readonly List<int> _triangles = new();

  private Mesh _mesh;
  private MeshFilter _meshFilter;
  private MeshRenderer _meshRenderer;
}
