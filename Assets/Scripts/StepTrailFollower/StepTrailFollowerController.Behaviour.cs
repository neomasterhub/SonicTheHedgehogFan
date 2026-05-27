using UnityEngine;

/// <summary>
/// Behaviour.
/// </summary>
public partial class StepTrailFollowerController
  : IStepTrailFollower
{
  public Vector3 Origin
  {
    get => _origin;
    set => _origin = value;
  }
}
