public interface IEnemySensorSystem : INextUpdater<IEnemyAI>
{
  void Update(EnemySensorContext context);
  void Apply();
  void Draw();
}
