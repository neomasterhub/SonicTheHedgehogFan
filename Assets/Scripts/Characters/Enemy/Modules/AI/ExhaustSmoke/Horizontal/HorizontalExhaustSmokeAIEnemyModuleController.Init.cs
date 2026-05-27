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

    _exhaustSmokeAnimator = exhaustSmoke.GetComponent<Animator>();
    _exhaustSmokeSpriteRenderer = exhaustSmoke.GetComponent<SpriteRenderer>();
    _exhaustSmokeTransform = exhaustSmoke.transform;
    _exhaustSmokePosition = transform.position + _exhaustSmokeOrigin;
    _exhaustSmokeVisibility = !_context.IsStatic;
  }
}
