using UnityEngine;

/// <summary>
/// Data.
/// </summary>
public partial class EdgeColliderRendererController : MonoBehaviour
{
  private bool _debugMode;
  private IMeshRenderer _meshRenderer;
  private EdgeCollider2D[] _edgeColliders;

  [SerializeField]
  private Color _color;
  [SerializeField]
  private float _width;
}
