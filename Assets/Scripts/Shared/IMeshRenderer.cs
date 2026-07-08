using UnityEngine;

public interface IMeshRenderer
{
  void DrawLine(Vector3 start, Vector3 end, float width, Color color);
  void DrawPolygon(Vector3[] points, Color color);
}
