/// <summary>
/// Implementation.
/// </summary>
public partial class HorizontalPatrolSpeedSystemController
  : IEnemySpeedSystem
{
  public float SpeedX { get; private set; }
  public float SpeedY { get; private set; }
  public EnemySpeedSystemType Type => EnemySpeedSystemType.HorizontalPatrol;

  public void UpdateSystem(EnemySensorContext context)
  {
    throw new System.NotImplementedException();
  }
}
