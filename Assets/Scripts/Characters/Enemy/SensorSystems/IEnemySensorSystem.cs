public interface IEnemySensorSystem : INextUpdater<IEnemyAI>
{
  void SetContext(EnemySensorContext context);
  void Apply();
  void Draw();
}
