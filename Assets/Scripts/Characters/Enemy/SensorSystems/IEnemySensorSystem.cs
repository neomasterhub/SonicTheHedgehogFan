public interface IEnemySensorSystem : INextUpdater<IEnemyAI>
{
  EnemySensorSystemType Type { get; }
  void UpdateSystem(EnemySensorContext context);
  void Apply();
  void Draw();
}
