using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Init.
/// </summary>
public partial class MeshRendererController
{
  private void Awake()
  {
    _mesh = new();
    _mesh.MarkDynamic();
    _meshFilter = this.AddComponent<MeshFilter>();
    _meshRenderer = this.AddComponent<MeshRenderer>();
    _meshRenderer.sortingOrder = _sortingOrder;
    _meshRenderer.sharedMaterial = new(Shader.Find("Sprites/Default"));
  }
}
