using UnityEngine;

public interface IEnemySensorSystem
{
  void SetParentPosition(Vector2 parentPosition);
  void Apply();
  void Draw();
}
