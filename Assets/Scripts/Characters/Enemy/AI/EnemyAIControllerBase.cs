public abstract class EnemyAIControllerBase<TAIContext>
  : AIControllerBase<TAIContext>,
  IEnemyAI
{
  protected EnemyAIControllerBase(EnemyAIType type)
  {
    Type = type;
    Effects = new();
  }

  public EnemyAIType Type { get; }
  public Pipeline Effects { get; }
}
