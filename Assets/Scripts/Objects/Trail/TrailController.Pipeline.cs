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
    if (Time.frameCount - _lastUpdateFrame > _delayFrameCount)
    {
      _lastUpdateFrame = Time.frameCount;
      _position = _target.position;
    }

    transform.position = _position;
  }
}
