using UnityEngine;

/// <summary>
/// Behaviour.
/// </summary>
public partial class StepTrailFollowerController
  : IStepTrailFollower
{
  public Vector3 Origin { set => _origin = value; }
}
