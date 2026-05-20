public interface IEnemySpeedSystem
{
  float SpeedX { get; }
  float SpeedY { get; }
  EnemySpeedSystemType Type { get; }
  void UpdateSystem(EnemySensorContext context);
}
