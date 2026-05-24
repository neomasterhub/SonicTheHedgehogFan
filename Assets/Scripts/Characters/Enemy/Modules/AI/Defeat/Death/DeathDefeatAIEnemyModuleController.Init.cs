using static EnemyConsts.Physics;

/// <summary>
/// Init.
/// </summary>
public partial class DeathDefeatAIEnemyModuleController
{
  public DeathDefeatAIEnemyModuleController()
    : base()
  {
    _deadActiveDuration = DeadActiveTimer;
  }

  public override void Initialize(EnemyControllerBase context)
  {
    base.Initialize(context);
    InitializeTimers();
  }

  private void InitializeTimers()
  {
    _deadActiveTimer = new Timer(_deadActiveDuration)
      .WhenCompleted(() => gameObject.SetActive(false));
  }
}
