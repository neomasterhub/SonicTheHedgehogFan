using System.Collections.Generic;
using UnityEngine;

public class SharedMeshRendererController : MonoBehaviour
{
  private readonly List<Vector3> _vertices = new();
  private readonly List<Color> _colors = new();
  private readonly List<int> _triangles = new();

  private Material _material;
  private Mesh _mesh;

  public static SharedMeshRendererController Instance { get; private set; }

  private void Awake()
  {
    if (Instance != null && Instance != this)
    {
      Destroy(gameObject);
      return;
    }

    Instance = this;

    InitializeComponents();
  }

  private void OnDestroy()
  {
    if (Instance == this)
    {
      Instance = null;
    }

    DestroyComponents();
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
  }

  private void InitializeComponents()
  {
    _material = new(Shader.Find("Sprites/Default"))
    {
      hideFlags = HideFlags.HideAndDontSave,
    };

    _mesh = new()
    {
      hideFlags = HideFlags.HideAndDontSave,
    };

    _mesh.MarkDynamic();
  }

  private void DestroyComponents()
  {
    if (_material != null)
    {
      Destroy(_material);
      _material = null;
    }

    if (_mesh != null)
    {
      Destroy(_mesh);
      _mesh = null;
    }
  }
}
