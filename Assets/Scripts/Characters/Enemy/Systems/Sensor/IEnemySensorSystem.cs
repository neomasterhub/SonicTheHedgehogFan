using UnityEngine;

public interface IEnemySensorSystem<TContext>
  : IEnemySystem<EnemySensorSystemType, TContext>
{
  void SetParentPosition(Vector2 parentPosition);
  void Apply();
  void Draw();
}
