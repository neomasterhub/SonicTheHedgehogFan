public interface IEnemySensorSystem<TContext>
  : IEnemySystem<EnemySensorSystemType, TContext>
{
  void Apply();
  void Draw();
}
