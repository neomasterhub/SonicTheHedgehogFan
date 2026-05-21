public abstract class EnemyAIControllerBase<TAIContext>
  : AIControllerBase<TAIContext>,
  IEnemyAI
{
  protected EnemyAIControllerBase(EnemyAISystemType type)
  {
    Type = type;
    Effects = new();
  }

  public EnemyAISystemType Type { get; }
  public Pipeline Effects { get; }
}
