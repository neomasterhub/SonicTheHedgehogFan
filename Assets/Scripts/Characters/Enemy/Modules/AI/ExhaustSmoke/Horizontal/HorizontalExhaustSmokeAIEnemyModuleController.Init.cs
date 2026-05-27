/// <summary>
/// Init.
/// </summary>
public partial class HorizontalExhaustSmokeAIEnemyModuleController
{
  public override void Initialize(EnemyControllerBase context)
  {
    base.Initialize(context);
    InitializeComponents();
  }

  private void InitializeComponents()
  {
    _exhaustSmoke = transform.Find("Exhaust Smoke").GetComponent<IStepTrailFollower>();
  }
}
