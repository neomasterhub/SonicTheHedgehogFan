using UnityEngine;

/// <summary>
/// Init.
/// </summary>
public partial class MotobugController
{
  public MotobugController()
    : base(new MotobugSpeedSystem())
  {
    _viewSystem = new();
  }

  protected override void InitializeComponents()
  {
    base.InitializeComponents();
    _viewSystem.SetComponents(GetComponent<Animator>());
  }
}
