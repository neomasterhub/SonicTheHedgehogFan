using UnityEngine;

/// <summary>
/// Pipeline.
/// </summary>
public partial class StepTrailFollowerController
{
  private void FixedUpdate()
  {
    _stepTime = Time.fixedDeltaTime;

    if (_stepTime > _stepDuration)
    {
      _position = _parentTransform.position + _origin;
    }

    transform.position = _position;
  }
}
