using UnityEngine;

/// <summary>
/// Pipeline.
/// </summary>
public partial class TrailController
{
  private void FixedUpdate()
  {
    UpdatePosition();
  }

  private void UpdatePosition()
  {
    _timer -= Time.fixedDeltaTime;

    if (_timer <= 0)
    {
      _timer = _delay;
      _position = _target.position;
    }

    transform.position = _position;
  }
}
