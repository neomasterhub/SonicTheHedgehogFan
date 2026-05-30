using UnityEngine;

/// <summary>
/// Pipeline.
/// </summary>
public partial class OffscreenDeactivatorController
{
  private void FixedUpdate()
  {
    if (IsOffscreenCheckIntervalElapsed() && IsOffscreen())
    {
      Deactivate();
    }
  }

  private bool IsOffscreenCheckIntervalElapsed()
  {
    _time += Time.fixedDeltaTime;

    if (_time > _offscreenCheckInterval)
    {
      _time = 0;
      return true;
    }

    return false;
  }

  private bool IsOffscreen()
  {
    var viewportPos = _camera.WorldToViewportPoint(transform.position);

    return viewportPos.x < 0
      || viewportPos.x > 1
      || viewportPos.y < 0
      || viewportPos.y > 1;
  }

  private void Deactivate()
  {
    gameObject.SetActive(false);
  }
}
