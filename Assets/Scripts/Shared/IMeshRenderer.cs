using UnityEngine;

public interface IMeshRenderer
{
  void DrawLine(Vector3 start, Vector3 end, float width, Color color);
  void DrawPolygon(Vector3[] points, Color color);
  void DrawSquare(Vector3 position, float halfSize, Color color);
}
