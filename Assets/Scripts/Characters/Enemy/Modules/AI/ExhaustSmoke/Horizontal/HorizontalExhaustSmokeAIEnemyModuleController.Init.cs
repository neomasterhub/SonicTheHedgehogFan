using UnityEngine;

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
    var exhaustSmoke = transform.Find("Exhaust Smoke");

    _exhaustSmokeTransform = exhaustSmoke.transform;
    _exhaustSmokeAnimator = exhaustSmoke.GetComponent<Animator>();
    _exhaustSmokePosition = transform.position + _exhaustSmokeOrigin;
  }
}
