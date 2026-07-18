using UnityEngine;

/// <summary>
/// Init.
/// </summary>
public partial class EdgeColliderRendererController
{
  public EdgeColliderRendererController()
  {
    _color = Color.green;
    _width = 0.3f;
  }

  private void Awake()
  {
    _edgeColliders = transform.GetComponentsInChildren<EdgeCollider2D>();
    _meshRenderer = GameObject.Find("Mesh Renderer").GetComponent<IMeshRenderer>();
  }
}
