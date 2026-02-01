using UnityEngine;

[RequireComponent(typeof(EdgeCollider2D))]
public class GroundController : MonoBehaviour
{
  private EdgeCollider2D _edgeCollider;

  private void Awake()
  {
    _edgeCollider = GetComponent<EdgeCollider2D>();
  }

  private void OnDrawGizmos()
  {
    if (_edgeCollider.pointCount < 2)
    {
      return;
    }

    Gizmos.color = Color.rosyBrown;

    var offset = _edgeCollider.offset;
    var transform = _edgeCollider.transform;
    var points = _edgeCollider.points;

    for (var i = 0; i < _edgeCollider.pointCount; i++)
    {
      Gizmos.DrawLine(
        transform.TransformPoint(offset + points[i]),
        transform.TransformPoint(offset + points[i + 1]));
    }
  }
}
