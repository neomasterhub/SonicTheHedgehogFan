using UnityEngine;

/// <summary>
/// Pipeline.
/// </summary>
public partial class StepTrailFollowerController
{
  private void FixedUpdate()
  {
    UpdatePosition();
  }

  private void UpdatePosition()
  {
    _stepTime += Time.fixedDeltaTime;

    if (_stepTime > _stepDuration)
    {
      _stepTime = 0;
      _position = _parentTransform.position + _origin;
    }

    transform.position = _position;
  }
}
