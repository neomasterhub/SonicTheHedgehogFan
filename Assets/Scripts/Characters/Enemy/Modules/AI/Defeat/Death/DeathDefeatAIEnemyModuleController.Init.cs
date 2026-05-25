using static EnemyConsts.Physics;

/// <summary>
/// Init.
/// </summary>
public partial class DeathDefeatAIEnemyModuleController
{
  public DeathDefeatAIEnemyModuleController()
    : base()
  {
    _dyingDuration = DyingDuration;
  }

  public override void Initialize(EnemyControllerBase context)
  {
    base.Initialize(context);
    InitializeTimers();
  }

  private void InitializeTimers()
  {
    _dyingTimer = new Timer(_dyingDuration)
      .WhenCompleted(() =>
      {
        _context.IsDying = false;
        _context.IsDead = true;
        gameObject.SetActive(false);
      });
  }
}
